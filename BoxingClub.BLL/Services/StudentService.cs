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
        public StudentService(IUnitOfWork uow)
        {
            Database = uow;
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
            return new StudentLiteDTO { Name = student.Name, Surname = student.Surname, Patronymic = student.Patronymic, BornDate = student.BornDate };//automapper
        }

        public IEnumerable<StudentLiteDTO> GetStudents()
        {
            var mapper = new MapperConfiguration(x => x.CreateMap<Student, StudentLiteDTO>()).CreateMapper();
            var collection = mapper.Map<IEnumerable<Student>, List<StudentLiteDTO>>(Database.Students.GetAll());
            return collection;
        }

        public void CreateStudent(CreateStudentDTO studentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
