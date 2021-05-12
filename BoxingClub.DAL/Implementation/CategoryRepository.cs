using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BoxingClubContext _db;

        public CategoryRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(Category item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Category is null");
            }

            await _db.Categories.AddAsync(item);
        }

        public void Delete(Category item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Category is null");
            }

            _db.Categories.Remove(item);
        }

        public Task<List<Category>> GetAllAsync()
        {
            return _db.Categories.Include(w => w.WeightCategory).Include(a => a.AgeCategory).ToListAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _db.Categories.Include(w => w.WeightCategory).Include(a => a.AgeCategory).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Category item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Category is null");
            }

            _db.Entry(item).State = EntityState.Modified;
        }

        public Task<List<Category>> GetCategoriesByTournamentIdAsync(int id)
        {
            return _db.Categories.AsQueryable().Where(x => x.TournamentId == id).Include(w => w.WeightCategory).Include(a => a.AgeCategory).ToListAsync();
        }
    }
}
