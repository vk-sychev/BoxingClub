using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Repositories
{
    class StudentRepository : IStudentRepository
    {
        private readonly BoxingClubContext _db;

        public StudentRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Student is null");
            }
            var group = await _db.BoxingGroups.FindAsync(item.BoxingGroupId);
            item.BoxingGroup = group;
            await _db.Students.AddAsync(item);
        }

        public void Delete(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Student is null");
            }
            _db.Students.Remove(item);
        }

        public Task<Student> GetByIdAsync(int id)
        {
            return _db.Students.Include(x => x.BoxingGroup).Include(x => x.MedicalCertificates).SingleOrDefaultAsync(s => s.Id == id);
        }


        public Task<List<Student>> GetAllAsync()
        {
            return _db.Students.Include(x => x.BoxingGroup).Include(x => x.MedicalCertificates).ToListAsync();
        }

        public void Update(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Student is null");
            }
            _db.Entry(item).State = EntityState.Modified;
        }

        public Task<List<Student>> GetStudentsPaginatedAsync(int pageIndex, int pageSize)
        {
            var query = _db.Students.AsQueryable().Include(x => x.BoxingGroup);
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public Task<int> GetCountOfStudentsAsync()
        {
            var query = _db.Students.AsQueryable().Include(x => x.BoxingGroup);
            var count = query.CountAsync();
            return count;
        }
    }
}
