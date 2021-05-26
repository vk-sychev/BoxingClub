using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class StudentSelectionService : IStudentSelectionService
    {
        private readonly IUnitOfWork _database;
        private readonly ISpecificationClient _specificationClient;
        private readonly IStudentSpecification _fighterExperienceSpecification;
        private readonly IStudentSpecification _medicalCertificateSpecification;
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

        }

        public async Task<List<StudentFullDTO>> GetStudentsByTournamentId(int tournamentId)
        {
/*            if (tournamentId <= 0)
            {
                throw new ArgumentNullException(nameof(tournamentId), "Tournament id is null");
            }*/

            var tournament = await _database.Tournaments.GetByIdAsync(tournamentId);

            if (tournament == null)
            {
                throw new NotFoundException($"Tournament with id = {tournamentId} isn't found", "");
            }

            var specifications = await _specificationClient.GetTournamentSpecifications(tournamentId);
            var students = await _database.Students.GetAllAsync();
            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);

            var validatedStudents = ValidateStudentsInList(mappedStudents);
            var takenStudents = GetFilteredStudents(tournament.IsMedCertificateRequired, validatedStudents);

            var studentsForTournament = GetStudentsBySpecifications(takenStudents, specifications);

            return studentsForTournament;
        }

        private List<StudentFullDTO> ValidateStudentsInList(List<StudentFullDTO> students)
        {
            foreach (var student in students)
            {
                student.Experienced = _fighterExperienceSpecification.Validate(student);
                student.IsMedicalCertificateValid = _medicalCertificateSpecification.Validate(student);
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

        private List<StudentFullDTO> GetStudentsBySpecifications(List<StudentFullDTO> students, List<TournamentSpecification> specifications)
        {
            var takenStudents = new List<StudentFullDTO>();

            foreach (var spec in specifications)
            {
                /*foreach (var student in students.Where(student => _categorySpecification.IsValid(student, spec)))
                {
                    takenStudents.Add(student);
                }*/
                var validStudents = students.Where(student => _categorySpecification.IsValid(student, spec)); 
                takenStudents.AddRange(validStudents);
            }

            return takenStudents;
        }
    }
}
