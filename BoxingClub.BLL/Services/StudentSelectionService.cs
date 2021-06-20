using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using HttpClientAdapters.Interfaces;
using ArgumentException = BoxingClub.Infrastructure.Exceptions.ArgumentException;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class StudentSelectionService : IStudentSelectionService
    {
        private readonly IUnitOfWork _database;
        private readonly ISpecificationClient _specificationClient;
        private readonly IStudentClientAdapter _studentClientAdapter;
        private readonly IStudentSpecification _competitionSpecification;
        private readonly IMapper _mapper;

        public StudentSelectionService(IUnitOfWork uow,
                                       ISpecificationClient specificationClient,
                                       IStudentClientAdapter studentClientAdapter,
                                       IMapper mapper)
        {
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
            _specificationClient = specificationClient ?? throw new ArgumentNullException(nameof(specificationClient), "specificationClient is null");
            _studentClientAdapter = studentClientAdapter ?? throw new ArgumentNullException(nameof(studentClientAdapter), "studentClientAdapter is null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");

            _competitionSpecification = new CompetitionSpecification();
        }

        public async Task<List<StudentFullDTO>> GetStudentsByTournamentId(string token, int tournamentId)
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
                throw new InvalidOperationException($"Specifications for tournament with id = {tournamentId} are not found");
            }

            var mappedTournament = _mapper.Map<HttpClients.Models.Tournament>(tournament);
            var mappedSpecification = _mapper.Map<HttpClients.Models.SpecModels.TournamentSpecification>(specification);

            var response = await _studentClientAdapter.GetStudentsBySpecification(token, mappedTournament, mappedSpecification);
            ProcessResponse(response.StatusCode);

            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(response.Items);
            var tournamentRequests = await _database.TournamentRequests.GetTournamentRequestsByStudentIds(mappedStudents.Select(x => x.Id).ToList());
            var mappedtournamentRequests = _mapper.Map<List<TournamentRequestDTO>>(tournamentRequests);

            SetTournamentsToStudents(mappedStudents, mappedtournamentRequests);
            var studentsForTournament = GetFreeStudents(mappedStudents, tournament);

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

            var deleteTournamentRequests = GetStudentsForDeleting(tournament.TournamentRequests, students);
            _database.TournamentRequests.DeleteTournamentRequestsRange(deleteTournamentRequests);
            await _database.SaveAsync();
        }

        public async Task<List<StudentFullDTO>> GetSelectedStudentsByTournamentId(string token, int tournamentId)
        {
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var tournamentRequests = await _database.TournamentRequests.GetTournamentRequestsByTournamentId(tournamentId);
            if (!tournamentRequests.Any())
            {
                return new List<StudentFullDTO>();
            }

            var response = await _studentClientAdapter.GetStudentsByIds(token, tournamentRequests.Select(x => x.StudentId.Value).ToList());
            ProcessResponse(response.StatusCode);

            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(response.Items);
            return mappedStudents;
        }

        public async Task DeleteTournamentRequest(int tournamentId)
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

            var tournamentRequests = await _database.TournamentRequests.GetTournamentRequestsByTournamentId(tournamentId);

            _database.TournamentRequests.DeleteTournamentRequestsRange(tournamentRequests);
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


        
        private List<StudentFullDTO> GetFreeStudents(List<StudentFullDTO> students, Tournament tournament)
        {
            return students.Where(x => _competitionSpecification.Validate(x, tournament)).ToList();
        }

        private List<TournamentRequest> GetStudentsForDeleting(List<TournamentRequest> tournamentRequests, List<StudentFullDTO> students)
        {

            return tournamentRequests.Where(s => !students.Any(m => s.StudentId == m.Id)).ToList();
        }

        private void SetTournamentsToStudents(List<StudentFullDTO> students, List<TournamentRequestDTO> requests)
        {
            if (!students.Any())
            {
                return;
            }

            if (!requests.Any())
            {
                return;
            }

            students.ForEach(student =>
            {
                requests.Where(request => request.StudentId == student.Id).ToList().ForEach(request =>
                {
                    if (request != null)
                    {
                        student.Tournaments.Add(request.Tournament);
                    }
                });
            });
        }

        private void ProcessResponse(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    throw new InvalidOperationException("Unauthorized!");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }
        }

    }
}
