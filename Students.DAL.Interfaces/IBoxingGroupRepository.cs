using System.Collections.Generic;
using System.Threading.Tasks;
using Students.DAL.Entities;

namespace Students.DAL.Interfaces
{
    public interface IBoxingGroupRepository : IRepository<BoxingGroup>
    {
        Task<BoxingGroup> GetBoxingGroupWithStudentsByIdAsync(int id);

        Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdAsync(string id);

        Task<List<BoxingGroup>> GetBoxingGroupsPaginatedAsync(int pageIndex, int pageSize);

        Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdPaginatedAsync(string id, int pageIndex, int pageSize);

        Task<int> GetCountOfBoxingGroupsAsync();

        Task<int> GetCountOfBoxingGroupsByCoachIdAsync(string id);
    }
}
