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
        private readonly BoxingClubContext _db;

        public StudentRepository(BoxingClubContext context)
        {
            _db = context;
        }


        public async Task Create(Student item)
        {
            await _db.Students.AddAsync(item);
        }

        public async Task<bool> Delete(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student != null)
            {
                _db.Students.Remove(student);
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<Student>> Find(Func<Student, ValueTask<bool>> predicate)
        {
            return await _db.Students.WhereAwait(predicate).ToListAsync();
        }


        public async Task<Student> Get(int id)
        {
            return await _db.Students.FindAsync(id);
        }


        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _db.Students.AsQueryable().ToListAsync();
        }

        public void Update(Student item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
