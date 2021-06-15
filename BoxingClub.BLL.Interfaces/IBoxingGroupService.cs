using BoxingClub.BLL.DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IBoxingGroupService
    {
        Task CreateBoxingGroupAsync(BoxingGroupDTO groupDTO);

        Task DeleteBoxingGroupAsync(int id);

        Task<BoxingGroupDTO> GetBoxingGroupByIdAsync(int id, string token);

        Task<List<BoxingGroupDTO>> GetBoxingGroupsAsync(string token);

        Task<List<BoxingGroupDTO>> GetBoxingGroupsByCoachIdAsync(string coachId, string token);

        Task UpdateBoxingGroupAsync(BoxingGroupDTO groupDTO);

        Task<BoxingGroupDTO> GetBoxingGroupWithStudentsByIdAsync(int id, string token);

        Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsPaginatedAsync(SearchModelDTO searchDTO, string token);

        Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsByCoachIdPaginatedAsync(string coachName, SearchModelDTO searchDTO, string token);
    }
}