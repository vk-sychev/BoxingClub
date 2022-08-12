using System.Threading.Tasks;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.WebManagers.Interfaces
{
    public interface IHomeWebManager
    {
        Task<PageViewModel<BoxingGroupDTO>> GetBoxingGroupsAsync(SearchModelDTO searchModel, string token);

        Task<PageViewModel<BoxingGroupDTO>> GetBoxingGroupsByCoachIdAsync(string coachName, SearchModelDTO searchModel, string token);
    }
}