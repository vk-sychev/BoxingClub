using System.Threading.Tasks;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.WebManagers.Interfaces
{
    public interface IHomeWebManager
    {
        Task<PageViewModel<BoxingGroupLiteViewModel>> GetBoxingGroupsAsync(SearchModelDTO searchModel, string token);

        Task<PageViewModel<BoxingGroupLiteViewModel>> GetBoxingGroupsByCoachIdAsync(string coachName, SearchModelDTO searchModel, string token);
    }
}