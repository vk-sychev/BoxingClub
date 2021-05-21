using BoxingClub.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        public Task<List<Category>> GetCategoriesByIds(List<Category> categoriesIds);
    }
}
