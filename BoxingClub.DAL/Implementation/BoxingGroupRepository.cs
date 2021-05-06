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
            await _db.BoxingGroups.AddAsync(item);
        }

        public void Delete(BoxingGroup item)
        {
            _db.BoxingGroups.Remove(item);
        }

        public async Task<BoxingGroup> GetByIdAsync(int id)
        {
            var res = await _db.BoxingGroups.AsQueryable().Where(g => g.Id == id).Include(x => x.Coach).SingleOrDefaultAsync();
            return res;
        }

        public async Task<IEnumerable<BoxingGroup>> GetAllAsync()
        {
            return await _db.BoxingGroups.Include(x => x.Coach).ToListAsync();
        }

        public async Task<BoxingGroup> GetBoxingGroupWithStudentsByIdAsync(int id)
        {
            var res = await _db.BoxingGroups.AsQueryable().Where(x => x.Id == id).Include(x => x.Coach).Include(x => x.Students).SingleOrDefaultAsync();
            return res;
        }

        public void Update(BoxingGroup item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdAsync(string id)
        {
            var groups = await _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id).ToListAsync();
            return groups;
        }

        public async Task<List<BoxingGroup>> GetBoxingGroupsPaginatedAsync(int pageIndex, int pageSize)
        {
            var query = _db.BoxingGroups.Include(x => x.Coach);
            var list = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<int> GetCountOfBoxingGroupsAsync()
        {
            var query = _db.BoxingGroups.AsQueryable().Include(x => x.Coach);
            var count = await query.CountAsync();
            return count;
        }

        public async Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdPaginatedAsync(string id, int pageIndex, int pageSize)
        {
            var query = _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id);
            var list = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<int> GetCountOfBoxingGroupsByCoachIdAsync(string id)
        {
            var query = _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id);
            var count = await query.CountAsync();
            return count;
        }
    }
}
