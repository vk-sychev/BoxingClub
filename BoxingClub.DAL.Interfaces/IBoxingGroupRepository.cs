using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IBoxingGroupRepository : IRepository<BoxingGroup>
    {
        Task<BoxingGroup> GetBoxingGroupWithStudentsAsync(int id);

        Task<List<BoxingGroup>> GetBoxingGroupsByCoachAsync(string id);
    }
}
