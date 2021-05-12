using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Implementation.Implementation
{
    class AgeCategoryRepository : IAgeCategoryRepository
    {
        private readonly BoxingClubContext _db;

        public AgeCategoryRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(AgeCategory item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "AgeCategory is null");
            }

            await _db.AgeCategories.AddAsync(item);
        }

        public void Delete(AgeCategory item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "AgeCategory is null");
            }

            _db.AgeCategories.Remove(item);
        }

        public Task<List<AgeCategory>> GetAllAsync()
        {
            return _db.AgeCategories.ToListAsync();
        }

        public Task<AgeCategory> GetByIdAsync(int id)
        {
            return _db.AgeCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(AgeCategory item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "AgeCategory is null");
            }
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
