using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System.Collections.Generic;
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
    }
}
