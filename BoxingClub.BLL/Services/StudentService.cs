using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database; 
        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public async Task<StudentFullDTO> GetStudentAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Student's id is null");    
            }
            var student = await _database.Students.GetAsync(id.Value);
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
            if(! await _database.Students.DeleteAsync(id.Value))
            {
                throw new NotFoundException($"Student with id = {id.Value} isn't found", "");
            }
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

        public async Task DeleteFromGroupAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Student's id is null");
            }

            var student = await _database.Students.GetAsync(id.Value);
            student.BoxingGroup = null;
            _database.Students.Update(student);
            await _database.SaveAsync();
        }
    }
}
