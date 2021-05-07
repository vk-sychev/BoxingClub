using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Repositories
{
    public class BoxingGroupRepository : IBoxingGroupRepository
    {
        private readonly BoxingClubContext _db;

        public BoxingGroupRepository(BoxingClubContext context)
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

        public Task<BoxingGroup> GetByIdAsync(int id)
        {
            return _db.BoxingGroups.Include(x => x.Coach).SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<List<BoxingGroup>> GetAllAsync()
        {
            return _db.BoxingGroups.Include(x => x.Coach).ToListAsync();
        }

        public Task<BoxingGroup> GetBoxingGroupWithStudentsByIdAsync(int id)
        {
           return _db.BoxingGroups.Include(x => x.Coach).Include(x => x.Students).SingleOrDefaultAsync(x => x.Id == id);
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

        public Task<List<BoxingGroup>> GetBoxingGroupsPaginatedAsync(int pageIndex, int pageSize)
        {
            var query = _db.BoxingGroups.Include(x => x.Coach);
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public Task<int> GetCountOfBoxingGroupsAsync()
        {
            var query = _db.BoxingGroups.AsQueryable().Include(x => x.Coach);
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
