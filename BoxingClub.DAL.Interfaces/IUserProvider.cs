using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUserProvider
    {
        Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);

        Task<bool> DeleteUserAsync(string id);

        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName);

        Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName); 

        Task<ApplicationUser> FindUserByIdAsync(string id);

        Task<string> GetUserRole(ApplicationUser user);

        Task<List<ApplicationUser>> GetUsersAsync();

        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName);

        Task<IdentityResult> SignUpAsync(ApplicationUser user, string password, string roleName);

        Task<ApplicationUser> GetUserByNameAsync(string name);
    }
}
