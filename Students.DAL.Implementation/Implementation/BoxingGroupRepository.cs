using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.DAL.Entities;
using Students.DAL.Implementation.EF;
using Students.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.DAL.Implementation.Implementation
{
    public class BoxingGroupRepository : IBoxingGroupRepository
    {
        private readonly StudentsContext _db;

        public BoxingGroupRepository(StudentsContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(BoxingGroup item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "BoxingGroup is null");
            }
            await _db.BoxingGroups.AddAsync(item);
        }

        public void Delete(BoxingGroup item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "BoxingGroup is null");
            }
            _db.BoxingGroups.Remove(item);
        }

        public Task<BoxingGroup> GetByIdAsync(int id) //FIX
        {
            return _db.BoxingGroups.AsQueryable().SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<List<BoxingGroup>> GetAllAsync()//FIX
        {
            return _db.BoxingGroups.AsQueryable().ToListAsync();
        }

        public Task<BoxingGroup> GetBoxingGroupWithStudentsByIdAsync(int id)//FIX
        {
           return _db.BoxingGroups.Include(x => x.Students).SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(BoxingGroup item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "BoxingGroup is null");
            }
            _db.Entry(item).State = EntityState.Modified;
        }

        public Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdAsync(string id)
        {
            return _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id).ToListAsync();
        }

        public Task<List<BoxingGroup>> GetBoxingGroupsPaginatedAsync(int pageIndex, int pageSize)//FIX
        {
            var query = _db.BoxingGroups.AsQueryable();
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public Task<int> GetCountOfBoxingGroupsAsync()//FIX
        {
            var query = _db.BoxingGroups.AsQueryable();
            var count = query.CountAsync();
            return count;
        }

        public Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdPaginatedAsync(string id, int pageIndex, int pageSize)
        {
            var query = _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id);
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public Task<int> GetCountOfBoxingGroupsByCoachIdAsync(string id)
        {
            var query = _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id);
            var count = query.CountAsync();
            return count;
        }
    }
}
