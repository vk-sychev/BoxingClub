using System.Threading.Tasks;
using IdentityServer.BLL.Entities;
using IdentityServer.Models;

namespace IdentityServer.WebManagers.Interfaces
{
    public interface IAdministrationWebManager
    {
        Task<PageViewModel<UserViewModel>> GetUsersAsync(SearchModelDTO searchModel);
    }
}