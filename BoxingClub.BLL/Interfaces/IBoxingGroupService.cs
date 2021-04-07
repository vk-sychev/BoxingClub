using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IBoxingGroupService
    {
        Task CreateGroup(BoxingGroupDTO groupDTO);
        Task DeleleGroup(int? id);
        Task<BoxingGroupDTO> GetBoxingGroup(int? id);
        Task<List<BoxingGroupDTO>> GetBoxingGroups();
        Task UpdateGroup(BoxingGroupDTO groupDTO);
    }
}