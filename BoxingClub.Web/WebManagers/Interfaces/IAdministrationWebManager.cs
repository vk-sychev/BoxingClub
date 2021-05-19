using BoxingClub.BLL.DomainEntities;
using BoxingClub.Web.Models;
using System.Threading.Tasks;

namespace BoxingClub.Web.WebManagers.Interfaces
{
    public interface IAdministrationWebManager
    {
        Task<PageViewModel<UserViewModel>> GetUsersAsync(SearchModelDTO searchModel);
    }
}