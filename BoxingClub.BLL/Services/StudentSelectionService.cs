﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using Itenso.TimePeriod;
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
            if (tournamentId <= 0)
            {
                throw new ArgumentException("tournamentId less or equal 0", nameof(tournamentId));
            }

            var tournament = await _database.Tournaments.GetByIdAsync(tournamentId);

            if (tournament == null)
            {
                throw new NotFoundException($"Tournament with id = {tournamentId} isn't found", "");
            }

            var specification = await GetTournamentSpecification(tournamentId);

            var students = await _database.Students.GetStudentsWithTournamentsAsync(); //get free students
            var freeStudents = GetFreeStudents(students, tournament);
            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);

            var validatedStudents = ValidateStudentsInList(mappedStudents);
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
                throw new NotFoundException($"Tournament with id = {tournamentId} isn't found", "");
            }

            var studentsFromDb = tournament.Students;
            var mappedStudents = _mapper.Map<List<Student>>(students);

            var deleteStudents = studentsFromDb.Where(s => !mappedStudents.Any(m => s.Id == m.Id)).ToList();

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

            var tournament = await _database.Tournaments.GetByIdAsync(tournamentId);

            if (tournament == null)
            {
                throw new NotFoundException($"Tournament with id = {tournamentId} isn't found", "");
            }

            tournament.Students = null;
            await _database.SaveAsync();
        }

        private async Task<TournamentSpecification> GetTournamentSpecification(int tournamentId)
        {
            var specification = new TournamentSpecification();

            try
            {
                specification = await _specificationClient.GetTournamentSpecifications(tournamentId);
            }
            catch
            {
                throw new InvalidOperationException("An error occurred while processing student selection.");
            }

            return specification;
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

        private List<StudentFullDTO> GetStudentsBySpecifications(List<StudentFullDTO> students, TournamentSpecification specification)
        {
            var takenStudents = new List<StudentFullDTO>();

            foreach (var ageGroup in specification.AgeGroups)
            {
                foreach (var student in students.Where(student => _categorySpecification.IsValid(student, ageGroup)))
                {
                    takenStudents.Add(student);
                }
/*                var validStudents = students.Where(student => _categorySpecification.IsValid(student, spec)); 
                takenStudents.AddRange(validStudents);*/
            }

            return takenStudents;
        }

        private List<Student> GetFreeStudents(List<Student> students, Tournament tournament)
        {
            var freeStudents = new List<Student>();

            foreach (var student in students)
            {
                var lastTournament = student.Tournaments.OrderBy(x => x.Date).LastOrDefault();
                if (lastTournament == null)
                {
                    freeStudents.Add(student);
                } 
                else if (new DateDiff(lastTournament.Date, tournament.Date).Days >= 10) //constant!
                {
                    freeStudents.Add(student);
                }
            }

            return freeStudents;
        }

    }
}
