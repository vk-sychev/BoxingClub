using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClud.Exceptions.Exceptions;
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
        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        IUnitOfWork Database { get; set; }

        public async Task<StudentFullDTO> GetStudent(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Student's id is null");    
            }
            var student = await Database.Students.Get(id.Value);
            if (student == null)
            {
                throw new NotFoundException($"Student with id = {id.Value} isn't found", "");
            }
            return _mapper.Map<StudentFullDTO>(student);
        }

        public async Task<IEnumerable<StudentLiteDTO>> GetStudents()
        {
            var students = await Database.Students.GetAll();
            var collection = _mapper.Map<IEnumerable<Student>, List<StudentLiteDTO>>(students);
            return collection;
        }

        public async Task CreateStudent(StudentFullDTO studentDTO)
        {
            if (studentDTO == null)
            {
                throw new ArgumentNullException(nameof(studentDTO), "Student is null");
            }
            var student = _mapper.Map<Student>(studentDTO);
            await Database.Students.Create(student);
            await Database.Save();
        }

        public Task DeleteStudent(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Student's id is null");
            }
            if(!Database.Students.Delete(id.Value))
            {
                throw new NotFoundException($"Student with id = {id.Value} isn't found", "");
            }
            return Database.Save();
        }

        public Task UpdateStudent(StudentFullDTO studentDTO)
        {
            if (studentDTO == null)
            {
                throw new ArgumentNullException(nameof(studentDTO), "Student is null");
            }
            var student = _mapper.Map<Student>(studentDTO);
            Database.Students.Update(student);
            return Database.Save();
        }
    }
}
