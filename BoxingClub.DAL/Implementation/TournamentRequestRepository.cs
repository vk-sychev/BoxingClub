using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class TournamentRequestRepository: ITournamentRequestRepository
    {
        private readonly BoxingClubContext _db;

        public TournamentRequestRepository(BoxingClubContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), "context is null");
        }

        public Task<List<TournamentRequest>> GetAllAsync()
        {
            return _db.TournamentRequests.ToListAsync();
        }

        public Task<TournamentRequest> GetByIdAsync(int id)
        {
            return _db.TournamentRequests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(TournamentRequest item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "TournamentRequest is null");
            }

            await _db.TournamentRequests.AddAsync(item);
        }

        public async Task CreateTournamentRequestRangeAsync(List<TournamentRequest> requests)
        {
            await _db.TournamentRequests.AddRangeAsync(requests);
        }

        public void Update(TournamentRequest item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "TournamentRequest is null");
            }

            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TournamentRequest item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "TournamentRequest is null");
            }

            _db.TournamentRequests.Remove(item);
        }
    }
}
