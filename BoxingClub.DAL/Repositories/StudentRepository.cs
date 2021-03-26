using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    class StudentRepository : IRepository<Student>
    {
        private BoxingClubContext db;

        public StudentRepository(BoxingClubContext context)
        {
            this.db = context;
        }


        public async Task Create(Student item)
        {
            await db.Students.AddAsync(item);
        }

        public bool Delete(int id)
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<Student>> Find(Func<Student, ValueTask<bool>> predicate)
        {
            return await db.Students.WhereAwait(predicate).ToListAsync();
        }


        public async Task<Student> Get(int id)
        {
            return await db.Students.FindAsync(id);
        }


        public async Task<IEnumerable<Student>> GetAll()
        {
            return await db.Students.AsQueryable().ToListAsync();
        }

        public void Update(Student item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
