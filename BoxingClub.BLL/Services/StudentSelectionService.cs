using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentException = BoxingClub.Infrastructure.Exceptions.ArgumentException;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class StudentSelectionService : IStudentSelectionService
    {
        private readonly IUnitOfWork _database;
        private readonly ISpecificationClient _specificationClient;
        private readonly IStudentSpecification _fighterExperienceSpecification;
        private readonly IStudentSpecification _medicalCertificateSpecification;
        private readonly IStudentSpecification _competitionSpecification;
        private readonly ICategorySpecification _categorySpecification;
        private readonly IMapper _mapper;

        public StudentSelectionService(IUnitOfWork uow,
                                       ISpecificationClient specificationClient,
                                       IMapper mapper)
        {
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
            _specificationClient = specificationClient ?? throw new ArgumentNullException(nameof(specificationClient), "specificationClient is null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");

            _medicalCertificateSpecification = new MedicalCertificateSpecification();
            _fighterExperienceSpecification = new FighterExperienceSpecification();
            _categorySpecification = new CategorySpecification();
            _competitionSpecification = new CompetitionSpecification();
        }

        public async Task<List<StudentFullDTO>> GetStudentsByTournamentId(int tournamentId)
        {
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var tournament = await _database.Tournaments.GetByIdAsync(tournamentId);

            if (tournament == null)
            {
                throw new NotFoundException($"Tournament with id = {tournamentId} isn't found", "");
            }

            var specification = await _specificationClient.GetTournamentSpecifications(tournamentId);
            if (specification == null)
            {
                throw new InvalidOperationException("Error occurred while selecting students for tournament");
            }

            var students = await _database.Students.GetStudentsWithTournamentsAsync();
            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);

            var freeStudents = GetFreeStudents(mappedStudents, tournament);
            
            var validatedStudents = ValidateStudentsInList(freeStudents, tournament);
            var takenStudents = GetFilteredStudents(tournament.IsMedCertificateRequired, validatedStudents);

            var studentsForTournament = GetStudentsBySpecifications(takenStudents, specification);

            return studentsForTournament;
        }

        public async Task CreateTournamentRequest(int tournamentId, List<StudentFullDTO> students)
        {
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var tournamentRequests = GetTournamentRequests(students, tournamentId);

            var mappedTournamentRequests = _mapper.Map<List<TournamentRequest>>(tournamentRequests);

            await _database.TournamentRequests.CreateTournamentRequestRangeAsync(mappedTournamentRequests);
            await _database.SaveAsync();
        }

        public async Task UpdateTournamentRequest(int tournamentId, List<StudentFullDTO> students)
        {
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var tournament = await _database.Tournaments.GetTournamentByIdWithStudentsAsync(tournamentId);

            if (tournament == null)
            {
                throw new InvalidOperationException($"Tournament with id = {tournamentId} isn't found");
            }

            var deleteStudents = GetStudentsForDeleting(tournament.Students, students);

            foreach (var student in deleteStudents)
            {
                tournament.Students.Remove(student);
            }

            await _database.SaveAsync();
        }

        public async Task<List<StudentFullDTO>> GetSelectedStudentsByTournamentId(int tournamentId)
        {
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var students = await _database.Students.GetStudentsByTournamentIdAsync(tournamentId);
            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);
            return mappedStudents;
        }

        public async Task DeleteAcceptedTournament(int tournamentId)
        {
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var tournament = await _database.Tournaments.GetTournamentByIdWithStudentsAsync(tournamentId);

            if (tournament == null)
            {
                throw new InvalidOperationException($"Tournament with id = {tournamentId} isn't found");
            }

            tournament.Students = null;
            await _database.SaveAsync();
        }

        private List<TournamentRequestDTO> GetTournamentRequests(List<StudentFullDTO> students, int tournamentId)
        {
            var tournamentRequests = new List<TournamentRequestDTO>();

            foreach (var student in students)
            {
                var tournamentRequest = new TournamentRequestDTO()
                {
                    StudentId = student.Id,
                    TournamentId = tournamentId,
                    StudentWeight = student.Weight,
                    StudentHeight = student.Height
                };

                tournamentRequests.Add(tournamentRequest);
            }

            return tournamentRequests;
        }


        private List<StudentFullDTO> ValidateStudentsInList(List<StudentFullDTO> students, Tournament tournament)
        {
            foreach (var student in students)
            {
                student.Experienced = _fighterExperienceSpecification.Validate(student, tournament);
                student.IsMedicalCertificateValid = _medicalCertificateSpecification.Validate(student, tournament);
            }
            return students;
        }

        private List<StudentFullDTO> GetFilteredStudents(bool isMedCertificateRequired, List<StudentFullDTO> students)
        {
            if (isMedCertificateRequired)
            {
                students = students.Where(x => x.IsMedicalCertificateValid).ToList();
            }

            return students.Where(x => x.Experienced).ToList();
        }

        private List<StudentFullDTO> GetStudentsBySpecifications(List<StudentFullDTO> students, TournamentSpecification specification)
        {
            var takenStudents = new List<StudentFullDTO>();

            foreach (var ageGroup in specification.AgeGroups)
            {
                foreach (var student in students.Where(student => _categorySpecification.IsValid(student, ageGroup)))
                {
                    takenStudents.Add(student);
                }
            }

            return takenStudents;
        }

        private List<StudentFullDTO> GetFreeStudents(List<StudentFullDTO> students, Tournament tournament)
        {
            return students.Where(x => _competitionSpecification.Validate(x, tournament)).ToList();
        }

        private List<Student> GetStudentsForDeleting(List<Student> studentsFromDb, List<StudentFullDTO> students)
        {
            var mappedStudents = _mapper.Map<List<Student>>(students);
            return studentsFromDb.Where(s => !mappedStudents.Any(m => s.Id == m.Id)).ToList();
        }

    }
}
