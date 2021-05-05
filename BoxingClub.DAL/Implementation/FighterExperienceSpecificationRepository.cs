using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class FighterExperienceSpecificationRepository : IFighterExperienceSpecificationRepository
    {
        private readonly BoxingClubContext _db;

        public FighterExperienceSpecificationRepository(BoxingClubContext context)
        {
            _db = context;
        }

        public Task CreateAsync(FighterExperienceSpecification item)
        {
            throw new NotImplementedException();
        }

        public void Delete(FighterExperienceSpecification item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FighterExperienceSpecification>> GetAllAsync()
        {
            return await _db.FighterExperienceSpecifications.AsQueryable().ToListAsync();
        }

        public async Task<FighterExperienceSpecification> GetByIdAsync(int id)
        {
            return await _db.FighterExperienceSpecifications.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Update(FighterExperienceSpecification item)
        {
            throw new NotImplementedException();
        }
    }
}
