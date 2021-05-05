using AutoMapper;
using BoxingClub.BLL.DomainEntities;
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
        private readonly IUnitOfWork _database; 
        public StudentService(IUnitOfWork uow, 
                              IMapper mapper,
                              IStudentSpecification studentSpecification)
        {
            _database = uow;
            _mapper = mapper;
            _studentSpecification = studentSpecification;
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
            return _mapper.Map<StudentFullDTO>(student);
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

        public async Task<PageModelDTO<StudentLiteDTO>> GetStudentsPaginatedAsync(int pageIndex, int pageSize)
        {
            var students = await _database.Students.GetStudentsPaginatedAsync(pageIndex, pageSize);
            var studentDTOs = _mapper.Map<List<StudentFullDTO>>(students);
            var count = await _database.Students.GetCountOfStudentsAsync();

            var validatedStudents = await AreStudentsInListExperienced(studentDTOs);
            var mappedStudents = _mapper.Map<List<StudentLiteDTO>>(validatedStudents);

            var model = new PageModelDTO<StudentLiteDTO>() { Items = mappedStudents, Count = count };
            return model;
        }

        public async Task<PageModelDTO<StudentLiteDTO>> GetExperiencedStudentsPaginatedAsync(int pageIndex, int pageSize)
        {
            var students = await _database.Students.GetAllAsync();
            var studentDTOs = _mapper.Map<List<StudentFullDTO>>(students);
            var verifiedStudents = new List<StudentFullDTO>();
            var validatedStudents = await AreStudentsInListExperienced(studentDTOs);

            foreach (var student in validatedStudents)
            {
                if (student.Experienced)
                {
                    verifiedStudents.Add(student);
                }
            }
            var takenStudents = verifiedStudents.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var mappedStudents = _mapper.Map<List<StudentLiteDTO>>(takenStudents);
            var count = verifiedStudents.Count;

            var model = new PageModelDTO<StudentLiteDTO>() { Items = mappedStudents, Count = count };
            return model;
        }

        public async Task<PageModelDTO<StudentLiteDTO>> GetNewbiesStudentsPaginatedAsync(int pageIndex, int pageSize)
        {
            var students = await _database.Students.GetAllAsync();
            var studentDTOs = _mapper.Map<List<StudentFullDTO>>(students);
            var verifiedStudents = new List<StudentFullDTO>();
            var validatedStudents = await AreStudentsInListExperienced(studentDTOs);

            foreach (var student in validatedStudents)
            {
                if (!student.Experienced)
                {
                    verifiedStudents.Add(student);
                }
            }
            var takenStudents = verifiedStudents.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var mappedStudents = _mapper.Map<List<StudentLiteDTO>>(takenStudents);
            var count = verifiedStudents.Count;

            var model = new PageModelDTO<StudentLiteDTO>() { Items = mappedStudents, Count = count };
            return model;
        }

        private async Task<List<StudentFullDTO>> AreStudentsInListExperienced(List<StudentFullDTO> students)
        {
            foreach (var student in students)
            {
                student.Experienced = await _studentSpecification.IsValidAsync(student);
            }
            return students;
        }
    }
}
