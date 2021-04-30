using BoxingClub.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<Student>> GetStudentsPaginatedAsync(int pageIndex, int pageSize);

        Task<int> GetCountOfStudentsAsync();
    }
}
