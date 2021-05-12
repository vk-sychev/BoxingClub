using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        public Task<List<Category>> GetCategoriesByTournamentIdAsync(int id);
    }
}
