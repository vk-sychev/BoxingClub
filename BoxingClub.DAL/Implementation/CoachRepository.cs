using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateAsync(ApplicationUser item)
        {
            await _db.Users.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coach = await _db.Users.FindAsync(id);
            if (coach != null)
            {
                _db.Users.Remove(coach);
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser> GetAsync(int id)
        {
            var coach = await _db.Users.FindAsync(id);
            return coach;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var coachRole = await _db.Roles.AsQueryable().Where(x => x.Name == "Coach").SingleOrDefaultAsync();
            var coachesRole = await _db.UserRoles.AsQueryable().Where(x => x.RoleId == coachRole.Id).ToListAsync();
            var coaches = new List<ApplicationUser>();
            foreach(var item in coachesRole)
            {
                var user = await _db.Users.FindAsync(item.UserId);
                coaches.Add(user);
            }
            return coaches;
        }

        public void Update(ApplicationUser item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
