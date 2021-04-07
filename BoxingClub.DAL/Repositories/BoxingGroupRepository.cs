using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    public class BoxingGroupRepository : IRepository<BoxingGroup>
    {
        private readonly BoxingClubContext _db;

        public BoxingGroupRepository(BoxingClubContext context)
        {
            _db = context;
        }

        public Task Create(BoxingGroup item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BoxingGroup>> Find(Func<BoxingGroup, ValueTask<bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<BoxingGroup> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BoxingGroup>> GetAll()
        {
            return await _db.BoxingGroups.AsQueryable().ToListAsync();
        }

        public void Update(BoxingGroup item)
        {
            throw new NotImplementedException();
        }
    }
}
