using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.DomainEntities.Enums;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
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
        private readonly IStudentSpecification _studentSpecification;
        private readonly IMedicalCertificateService _medicalCertificateService;
        private readonly IUnitOfWork _database; 
        public StudentService(IUnitOfWork uow, 
                              IMapper mapper,
                              IStudentSpecification studentSpecification,
                              IMedicalCertificateService medicalCertificateService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
            _studentSpecification = studentSpecification ?? throw new ArgumentNullException(nameof(studentSpecification), "studentSpecification is null");
            _medicalCertificateService = medicalCertificateService ?? throw new ArgumentNullException(nameof(medicalCertificateService), "medicalCertificateService is null");
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

        public async Task<List<StudentLiteDTO>> GetStudentsAsync()
        {
            var students = await _database.Students.GetAllAsync();
            var collection = _mapper.Map<List<StudentLiteDTO>>(students);
            return collection;
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

        public async Task UpdateStudentAsync(StudentFullDTO studentDTO)
        {
            if (studentDTO == null)
            {
                throw new ArgumentNullException(nameof(studentDTO), "Student is null");
            }
            var student = _mapper.Map<Student>(studentDTO);
            _database.Students.Update(student);
            await _database.SaveAsync();
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



        public async Task<PageModelDTO<StudentLiteDTO>> GetStudentsAsync(SearchModelDTO searchDTO) //убрать async
        {
            if (searchDTO == null)
            {
                throw new ArgumentNullException(nameof(searchDTO), "SearchDTO is null");
            }

            var filterOrder = GetFilterOrder(searchDTO.Filter);

            if (searchDTO.PageIndex == null)
            {
                searchDTO.PageIndex = 1;
            }

            if (searchDTO.PageSize == null)
            {
                searchDTO.PageSize = 3;
            }

            var students = await _database.Students.GetAllAsync();
            var studentDTOs = _mapper.Map<List<StudentFullDTO>>(students);
            var validatedStudents = AreStudentsInListExperienced(studentDTOs);
            var mappedValidatedStudents = _mapper.Map<List<StudentLiteDTO>>(validatedStudents);
            
            if (filterOrder == FilterOrder.Experienced)
            {
                mappedValidatedStudents = mappedValidatedStudents.Where(it => it.Experienced).ToList();
            }

            if (filterOrder == FilterOrder.Newbies)
            {
                mappedValidatedStudents = mappedValidatedStudents.Where(it => !it.Experienced).ToList();
            }

            var takenStudents = mappedValidatedStudents.Skip((searchDTO.PageIndex.Value - 1) * searchDTO.PageSize.Value).Take(searchDTO.PageSize.Value);
            var count = mappedValidatedStudents.Count;

            if (takenStudents.Count() == 0)
            {
                searchDTO.PageIndex = 1;
                takenStudents = mappedValidatedStudents.Skip((searchDTO.PageIndex.Value - 1) * searchDTO.PageSize.Value).Take(searchDTO.PageSize.Value);
            }

            return new PageModelDTO<StudentLiteDTO>() { Items = takenStudents, Count = count };
        }


        /*private async Task<PageModelDTO<StudentLiteDTO>> GetStudentsPaginatedAsync(int pageIndex, int pageSize)
        {
            var students = await _database.Students.GetStudentsPaginatedAsync(pageIndex, pageSize);
            var studentDTOs = _mapper.Map<List<StudentFullDTO>>(students);
            var count = await _database.Students.GetCountOfStudentsAsync();

            var validatedStudents = AreStudentsInListExperienced(studentDTOs);
            var mappedStudents = _mapper.Map<List<StudentLiteDTO>>(validatedStudents);

            var model = new PageModelDTO<StudentLiteDTO>() { Items = mappedStudents, Count = count };
            return model;
        }*/

        private List<StudentFullDTO> AreStudentsInListExperienced(List<StudentFullDTO> students)
        {
            foreach (var student in students)
            {
                student.Experienced = _studentSpecification.IsValid(student);
            }
            return students;
        }

        private FilterOrder GetFilterOrder(int? filter)
        {
            switch (filter)
            {
                case 1:
                    return FilterOrder.Experienced;
                case 2:
                    return FilterOrder.Newbies;
                default:
                    return FilterOrder.All;
            }
        }
    }
}
