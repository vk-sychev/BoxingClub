using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    public class CoachRepository : ICoachRepository
    {
        private readonly BoxingClubContext _db;
        public CoachRepository (BoxingClubContext context)
        {
            _db = context;
        }

        public async Task CreateAsync(Coach item)
        {
            await _db.Coaches.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coach = await _db.Coaches.FindAsync(id);
            if (coach != null)
            {
                _db.Coaches.Remove(coach);
                return true;
            }
            return false;
        }

        public async Task<Coach> GetAsync(int id)
        {
            var coach = await _db.Coaches.FindAsync(id);
            return coach;
        }

        public async Task<IEnumerable<Coach>> GetAllAsync()
        {
            var coaches = await _db.Coaches.ToListAsync();
            return coaches;
        }

        public void Update(Coach item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
