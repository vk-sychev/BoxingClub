using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    class StudentRepository : IStudentRepository
    {
        private readonly BoxingClubContext _db;

        public StudentRepository(BoxingClubContext context)
        {
            _db = context;
        }

        public async Task CreateAsync(Student item)
        {
            var group = await _db.BoxingGroups.FindAsync(item.BoxingGroupId);
            item.BoxingGroup = group;
            await _db.Students.AddAsync(item);
        }

        public void Delete(Student item)
        {
            _db.Students.Remove(item);
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _db.Students.AsQueryable().Include(x => x.BoxingGroup).Where(s => s.Id == id).SingleOrDefaultAsync();
        }


        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _db.Students.AsQueryable().Include(x => x.BoxingGroup).ToListAsync();
        }

        public void Update(Student item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<Student>> GetStudentsPaginatedAsync(int pageIndex, int pageSize, Func<bool, bool> filter)
        {
            var query = _db.Students.AsQueryable().Include(x => x.BoxingGroup);
            var list = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<int> GetCountOfStudentsAsync(Expression<Func<Student, bool>> filter)
        {
            var query = _db.Students.AsQueryable().Include(x => x.BoxingGroup).Where(filter);
            var count = await query.CountAsync();
            return count;
        }
    }
}
