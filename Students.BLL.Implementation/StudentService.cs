using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Enums;
using BoxingClub.Infrastructure.Exceptions;
using Students.BLL.DomainEntities;
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
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentSpecification _fighterExperienceSpecification;
        private readonly IStudentSpecification _medicalCertificateSpecification;
        private readonly IUnitOfWork _database;

        public StudentService(IUnitOfWork uow, 
                              IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
            _medicalCertificateSpecification = new MedicalCertificateSpecification();
            _fighterExperienceSpecification = new FighterExperienceSpecification();
        }

        public async Task<StudentFullDTO> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Student's id less or equal 0", nameof(id));
            }

            var student = await _database.Students.GetByIdAsync(id);

            if (student == null)
            {
                throw new NotFoundException($"Student with id = {id} isn't found", "");
            }
            var mappedStudent = _mapper.Map<StudentFullDTO>(student);
            return mappedStudent;
        }


        public async Task CreateStudentAsync(StudentFullDTO studentDTO)
        {
            if (studentDTO == null)
            {
                throw new ArgumentNullException(nameof(studentDTO), "Student is null");
            }
            var student = _mapper.Map<Student>(studentDTO);
            
            await _database.Students.CreateAsync(student);
            await _database.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Students's id less or equal 0", nameof(id));
            }

            var student = await _database.Students.GetByIdAsync(id);

            if (student == null)
            {
                throw new InvalidOperationException($"Student with id = {id} isn't found");
            }
            _database.Students.Delete(student);
            await _database.SaveAsync();
        }

        public Task UpdateStudentAsync(StudentFullDTO studentDTO)
        {
            if (studentDTO == null)
            {
                throw new ArgumentNullException(nameof(studentDTO), "Student is null");
            }
            var student = _mapper.Map<Student>(studentDTO);
            _database.Students.Update(student);
            return _database.SaveAsync();
        }

        public async Task DeleteFromGroupAsync(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentException("Student's id less or equal 0", nameof(studentId));
            }

            var student = await _database.Students.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new InvalidOperationException($"Student with id = {studentId} isn't found");
            }

            student.BoxingGroup = null;
            _database.Students.Update(student);
            await _database.SaveAsync();
        }

        public async Task<PageModelDTO<StudentLiteDTO>> GetStudentsPaginatedAsync(SearchModelDTO searchDTO)
        {
            if (searchDTO == null)
            {
                throw new ArgumentNullException(nameof(searchDTO), "SearchDTO is null");
            }

            if (searchDTO.PageIndex == null)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
            }

            if (searchDTO.PageSize == null)
            {
                searchDTO.PageSize = PageModelConstants.PageSize;
            }

            var students = await _database.Students.GetAllAsync();
            
            var studentDTOs = _mapper.Map<List<StudentFullDTO>>(students);

            var validatedStudents = ValidateStudentsInList(studentDTOs);
            var mappedValidatedStudents = _mapper.Map<List<StudentLiteDTO>>(validatedStudents);

            var experienceOrder = GetExperienceOrder(searchDTO.ExperienceFilter);
            var medExaminationOrder = GetMedExaminationOrder(searchDTO.MedExaminationFilter);

            mappedValidatedStudents = GetFilteredStudents(experienceOrder, medExaminationOrder, mappedValidatedStudents);

            var takenStudents = mappedValidatedStudents.Skip((searchDTO.PageIndex.Value - 1) * searchDTO.PageSize.Value).Take(searchDTO.PageSize.Value);
            var count = mappedValidatedStudents.Count;

            if (!takenStudents.Any())
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
                takenStudents = mappedValidatedStudents.Skip((searchDTO.PageIndex.Value - 1) * searchDTO.PageSize.Value).Take(searchDTO.PageSize.Value);
            }

            return new PageModelDTO<StudentLiteDTO>() { Items = takenStudents, Count = count };
        }


        private List<StudentLiteDTO> GetFilteredStudents(ExperienceOrder experienceOrder, MedExaminationOrder medExaminationOrder, List<StudentLiteDTO> students)
        {
            var filteredByExperienceStudents = FilterByExperience(experienceOrder, students);
            var filteredByMedExamination = FilterByMedExamination(medExaminationOrder, filteredByExperienceStudents);
            return filteredByMedExamination;
        }

        private List<StudentLiteDTO> FilterByExperience(ExperienceOrder experienceOrder, List<StudentLiteDTO> students)
        {
            if (experienceOrder == ExperienceOrder.Experienced)
            {
                return students.Where(it => it.Experienced).ToList();
            }

            if (experienceOrder == ExperienceOrder.Newbies)
            {
                return students.Where(it => !it.Experienced).ToList();
            }

            return students;
        }


        private List<StudentLiteDTO> FilterByMedExamination(MedExaminationOrder medExaminationOrder, List<StudentLiteDTO> students)
        {
            if (medExaminationOrder == MedExaminationOrder.Successed)
            {
                return students.Where(it => it.IsMedicalCertificateValid).ToList();
            }

            if (medExaminationOrder == MedExaminationOrder.Failed)
            {
                return students.Where(it => !it.IsMedicalCertificateValid).ToList();
            }

            return students;
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

        private ExperienceOrder GetExperienceOrder(int? filter)
        {
            switch (filter)
            {
                case 1:
                    return ExperienceOrder.Experienced;
                case 2:
                    return ExperienceOrder.Newbies;
                default:
                    return ExperienceOrder.All;
            }
        }

        private MedExaminationOrder GetMedExaminationOrder(int? filter)
        {
            switch (filter)
            {
                case 1:
                    return MedExaminationOrder.Successed;
                case 2:
                    return MedExaminationOrder.Failed;
                default:
                    return MedExaminationOrder.All;
            }
        }
    }
}
