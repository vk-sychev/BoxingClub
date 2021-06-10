using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.DAL.Interfaces
{
    public interface IUserProvider
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        Task<ApplicationUser> FindUserByIdAsync(string id);

        Task<ApplicationUser> GetUserByNameAsync(string name);

        Task<List<ApplicationUser>> GetUsersAsync();

        Task<List<ApplicationUser>> GetUsersPaginatedAsync(int pageIndex, int pageSize);

        Task<int> GetCountOfUsersAsync();

        Task<IList<ApplicationUser>> GetUsersByRoleAsync(string roleName);

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

        bool IsSupportUserRole();
    }
}