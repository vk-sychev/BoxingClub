using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Enums;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Services
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

        public async Task<StudentFullDTO> GetStudentByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Student's id is null");    
            }

            var student = await _database.Students.GetByIdAsync(id.Value);

            if (student == null)
            {
                throw new NotFoundException($"Student with id = {id.Value} isn't found", "");
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

        public async Task DeleteStudentAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Student's id is null");
            }

            var student = await _database.Students.GetByIdAsync(id.Value);

            if (student == null)
            {
                throw new NotFoundException($"Student with id = {id.Value} isn't found", "");
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

        public async Task DeleteFromGroupAsync(int? studentId)
        {
            if (studentId == null)
            {
                throw new ArgumentNullException(nameof(studentId), "Student's id is null");
            }

            var student = await _database.Students.GetByIdAsync(studentId.Value);

            if (student == null)
            {
                throw new NotFoundException($"Student with id = {studentId.Value} isn't found", "");
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
            AssignLastMedicalCertificateForStudents(studentDTOs);

            var validatedStudents = ValidateStudentsInList(studentDTOs);
            var mappedValidatedStudents = _mapper.Map<List<StudentLiteDTO>>(validatedStudents);

            var experienceOrder = GetExperienceOrder(searchDTO.ExperienceFilter);
            var medExaminationOrder = GetMedExaminationOrder(searchDTO.MedExaminationFilter);

            mappedValidatedStudents = GetFilteredStudents(experienceOrder, medExaminationOrder, mappedValidatedStudents);

            var takenStudents = mappedValidatedStudents.Skip((searchDTO.PageIndex.Value - 1) * searchDTO.PageSize.Value).Take(searchDTO.PageSize.Value);
            var count = mappedValidatedStudents.Count;

            if (takenStudents.Count() == 0)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
                takenStudents = mappedValidatedStudents.Skip((searchDTO.PageIndex.Value - 1) * searchDTO.PageSize.Value).Take(searchDTO.PageSize.Value);
            }

            return new PageModelDTO<StudentLiteDTO>() { Items = takenStudents, Count = count };
        }


        private void AssignLastMedicalCertificateForStudents(List<StudentFullDTO> students)
        {
            foreach(var student in students)
            {
                student.LastMedicalCertificate = student.MedicalCertificates.OrderBy(x => x.DateOfIssue).LastOrDefault();
            }
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
                student.Experienced = _fighterExperienceSpecification.IsValid(student);
                student.IsMedicalCertificateValid = _medicalCertificateSpecification.IsValid(student);
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
