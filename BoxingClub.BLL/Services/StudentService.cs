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
        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<StudentFullDTO> GetStudent(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "id is null");    
            }
            var student = await Database.Students.Get(id.Value);
            if (student == null)
            {
                throw new NotFoundException("Student isn't found", "");
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
            var student = _mapper.Map<Student>(studentDTO);
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "student is null");
            }
            await Database.Students.Create(student);
            await Database.Save();
        }

        public async Task DeleteStudent(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "id is null");
            }
            Database.Students.Delete(id.Value);
            await Database.Save();
        }

        public async Task UpdateStudent(StudentFullDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "student is null");
            }
            Database.Students.Update(student);
            await Database.Save();
        }
    }
}
