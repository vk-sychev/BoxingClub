using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxingClub.DAL.Repositories
{
    class StudentRepository : IRepository<Student>
    {
        private BoxingClubContext db;

        public StudentRepository(BoxingClubContext context)
        {
            this.db = context;
        }

        public void Create(Student item)
        {
            db.Students.Add(item);
        }

        public void Delete(int id)
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
            }
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return db.Students.Where(predicate).ToList();
        }

        public Student Get(int id)
        {
            return db.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students;
        }

        public void Update(Student item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
