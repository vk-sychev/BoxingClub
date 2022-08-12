using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tournaments.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T item);

        void Update(T item);

        void Delete(T item);
    }
}
