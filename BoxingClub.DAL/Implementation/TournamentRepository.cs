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
    public class TournamentRepository : ITournamentRepository
    {
        private readonly BoxingClubContext _db;

        public TournamentRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public async Task CreateAsync(Tournament item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Tournament is null");
            }

            await _db.Tournaments.AddAsync(item);
        }

        public void Delete(Tournament item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Tournament is null");
            }

            _db.Tournaments.Remove(item);
        }

        public Task<List<Tournament>> GetAllAsync()
        {
            return _db.Tournaments.Include(c => c.Categories)
                                      .ThenInclude(x => x.Category)
                                          .ThenInclude(x => x.AgeWeightCategory)
                                              .ThenInclude(x => x.AgeCategory)
                                              .ToListAsync();
        }

        public Task<Tournament> GetByIdAsync(int id)
        {
            return _db.Tournaments.Include(c => c.Categories)
                                  .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Tournament item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Tournament is null");
            }
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
