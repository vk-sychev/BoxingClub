using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IBoxingGroupService
    {
        Task CreateGroupAsync(BoxingGroupDTO groupDTO);

        Task DeleleGroupAsync(int? id);

        Task<BoxingGroupDTO> GetBoxingGroupAsync(int? id);

        Task<List<BoxingGroupDTO>> GetBoxingGroupsAsync();

        Task UpdateGroupAsync(BoxingGroupDTO groupDTO);

        Task<BoxingGroupDTO> GetBoxingGroupWithStudentsAsync(int? id);
    }
}