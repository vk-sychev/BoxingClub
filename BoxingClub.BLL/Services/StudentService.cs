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
    class StudentService : IStudentService
    {
        public StudentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        IUnitOfWork Database { get; set; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IndexStudentDTO GetStudent(int? id)
        {
            throw new NotImplementedException();
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

        k   }
}
