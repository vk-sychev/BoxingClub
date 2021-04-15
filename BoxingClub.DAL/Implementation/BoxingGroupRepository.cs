using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace BoxingClub.DAL.Repositories
{
    public class BoxingGroupRepository : IBoxingGroupRepository
    {
        private readonly BoxingClubContext _db;

        public BoxingGroupRepository(BoxingClubContext context)
        {
            _db = context;
        }

        public async Task CreateAsync(BoxingGroup item)
        {
            await _db.BoxingGroups.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _db.BoxingGroups.FindAsync(id);
            if (group != null)
            {
                _db.BoxingGroups.Remove(group);
                return true;
            }
            return false;
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

        public async Task<BoxingGroup> GetBoxingGroupWithStudentsAsync(int id)
        {
            var res = await _db.BoxingGroups.AsQueryable().Where(x => x.Id == id).Include(x => x.Coach).Include(x => x.Students).SingleOrDefaultAsync();
            return res;
        }

        public void Update(BoxingGroup item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<BoxingGroup>> GetBoxingGroupsByCoachAsync(string id)
        {
            var groups = await _db.BoxingGroups.AsQueryable().Where(x => x.CoachId == id).ToListAsync();
            return groups;
        }
    }
}
