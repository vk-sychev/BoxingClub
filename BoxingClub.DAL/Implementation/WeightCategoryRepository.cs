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
    class WeightCategoryRepository : IWeightCategoryRepository
    {
        private readonly BoxingClubContext _db;

        public WeightCategoryRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(WeightCategory item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "WeightCategory is null");
            }

            await _db.WeightCategories.AddAsync(item);
        }

        public void Delete(WeightCategory item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "WeightCategory is null");
            }

            _db.WeightCategories.Remove(item);
        }

        public Task<List<WeightCategory>> GetAllAsync()
        {
            return _db.WeightCategories.ToListAsync();
        }

        public Task<WeightCategory> GetByIdAsync(int id)
        {
            return _db.WeightCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(WeightCategory item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "WeightCategory is null");
            }

            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
