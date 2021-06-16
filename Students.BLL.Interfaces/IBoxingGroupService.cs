using System.Collections.Generic;
using System.Threading.Tasks;
using Students.BLL.DomainEntities;

namespace Students.BLL.Interfaces
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

        Task<PageModelDTO<BoxingGroupDTO>> GetBoxingGroupsByCoachIdPaginatedAsync(string username, SearchModelDTO searchDTO, string token);
    }
}