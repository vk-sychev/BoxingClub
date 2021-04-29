using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IBoxingGroupRepository : IRepository<BoxingGroup>
    {
        Task<BoxingGroup> GetBoxingGroupWithStudentsByIdAsync(int id);

        Task<List<BoxingGroup>> GetBoxingGroupsByCoachIdAsync(string id);

        Task<List<BoxingGroup>> GetBoxingGroupsPaginatedAsync(int pageIndex, int pageSize);

        Task<int> GetCountOfBoxingGroupsAsync();
    }
}
