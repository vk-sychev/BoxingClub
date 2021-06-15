using BoxingClub.BLL.DomainEntities;
using BoxingClub.Web.Models;
using System.Threading.Tasks;

namespace BoxingClub.Web.WebManagers.Interfaces
{
    public interface IHomeWebManager
    {
        Task<PageViewModel<BoxingGroupLiteViewModel>> GetBoxingGroupsAsync(SearchModelDTO searchModel, string token);

        Task<PageViewModel<BoxingGroupLiteViewModel>> GetBoxingGroupsByCoachIdAsync(string coachName, SearchModelDTO searchModel, string token);
    }
}