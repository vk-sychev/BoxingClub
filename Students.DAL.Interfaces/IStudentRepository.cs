using System.Collections.Generic;
using System.Threading.Tasks;
using Students.DAL.Entities;

namespace Students.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<Student>> GetStudentsByIds(List<int> ids);
    }
}
