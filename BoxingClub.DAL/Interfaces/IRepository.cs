using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Find(Func<T, ValueTask<bool>> predicate);

        Task<T> Get(int id);

        Task Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}
