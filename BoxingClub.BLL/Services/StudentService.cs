using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
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

        public IndexStudentDTO GetStudent(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("id isn't set");
            }
            var student = Database.Students.Get(id.Value);
            if (student == null)
            {
                throw new ValidationException("Student din't find");
            }
            return new IndexStudentDTO { Name = student.Name, Surname = student.Surname, Patronymic = student.Patronymic, BornDate = student.BornDate };
        }

        public IEnumerable<IndexStudentDTO> GetStudents()
        {
            var mapper = new MapperConfiguration(x => x.CreateMap<Student, IndexStudentDTO>()).CreateMapper();
            var collection = mapper.Map<IEnumerable<Student>, List<IndexStudentDTO>>(Database.Students.GetAll());
            if (collection == null)
            {
                throw new ValidationException("List of students is null");
            }
            return collection;
        }
    }
}
