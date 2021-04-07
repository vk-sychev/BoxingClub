using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IBoxingGroupService
    {
        Task<List<BoxingGroupDTO>> GetBoxingGroups();

        Task<BoxingGroupDTO> GetBoxingGroup(int? id);
    }
}