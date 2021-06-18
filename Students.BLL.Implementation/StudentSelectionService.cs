using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Exceptions;
using Students.BLL.DomainEntities;
using Students.BLL.DomainEntities.SpecModels;
using Students.BLL.Implementation.Specifications;
using Students.BLL.Interfaces;
using Students.BLL.Interfaces.Specifications;
using Students.DAL.Entities;
using Students.DAL.Interfaces;
using ArgumentException = BoxingClub.Infrastructure.Exceptions.ArgumentException;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace Students.BLL.Implementation
{
    public class StudentSelectionService : IStudentSelectionService
    {
        private readonly IUnitOfWork _database;
        private readonly IStudentSpecification _fighterExperienceSpecification;
        private readonly IStudentSpecification _medicalCertificateSpecification;
        private readonly ICategorySpecification _categorySpecification;
        private readonly IMapper _mapper;

        public StudentSelectionService(IUnitOfWork uow,
                                       IMapper mapper)
        {
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");

            _medicalCertificateSpecification = new MedicalCertificateSpecification();
            _fighterExperienceSpecification = new FighterExperienceSpecification();
            _categorySpecification = new CategorySpecification();
        }

        public async Task<List<StudentFullDTO>> GetStudentsBySpecification(TournamentDTO tournament, TournamentSpecificationDTO specification)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "tournament is null");
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification), "specification is null");
            }

            var students = await _database.Students.GetAllAsync();
            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);

            var validatedStudents = ValidateStudentsInList(mappedStudents, tournament);
            var takenStudents = GetFilteredStudents(tournament.IsMedCertificateRequired, validatedStudents);

            var studentsForTournament = GetStudentsBySpecifications(takenStudents, specification);
            return studentsForTournament;
        }

        public async Task<List<StudentFullDTO>> GetStudentsByIds(List<int> studentsIds)
        {
            var students = await _database.Students.GetStudentsByIds(studentsIds);
            var mappedStudents = _mapper.Map<List<StudentFullDTO>>(students);
            return mappedStudents;
        }

        private List<StudentFullDTO> ValidateStudentsInList(List<StudentFullDTO> students, TournamentDTO tournament)
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

        private List<StudentFullDTO> GetStudentsBySpecifications(List<StudentFullDTO> students, TournamentSpecificationDTO specification)
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
    }
}
