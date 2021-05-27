using BoxingClub.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<Student>> GetFreeStudentsAsync();

        Task<List<Student>> GetStudentsByTournamentIdAsync(int id);
    }
}
