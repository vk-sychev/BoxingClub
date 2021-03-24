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

        public StudentLiteDTO GetStudent(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "id is null");    
            }
            var student = Database.Students.Get(id.Value);
            if (student == null)
            {
                throw new NotFoundException("Student isn't found", "");
            }
            return _mapper.Map<StudentLiteDTO>(student);
        }

        public IEnumerable<StudentLiteDTO> GetStudents()
        {
            var collection = _mapper.Map<IEnumerable<Student>, List<StudentLiteDTO>>(Database.Students.GetAll());
            return collection;
        }

        public void CreateStudent(CreateStudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "student is null");
            }
            Database.Students.Create(student);
            Database.Save();
        }

        public void DeleteStudent(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "id is null");
            }
            Database.Students.Delete(id.Value);
            Database.Save();
        }
    }
}
