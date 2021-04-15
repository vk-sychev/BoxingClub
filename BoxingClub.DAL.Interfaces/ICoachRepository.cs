using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface ICoachRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByNameAsync(string name);
    }
}
