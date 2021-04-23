using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IRoleProvider
    {
        Task<IdentityRole> FindRoleByIdAsync(string id);

        Task<IdentityRole> FindRoleByNameAsync(string roleName);

        Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);

        Task<string> GetUserRole(ApplicationUser user);

        Task<List<IdentityRole>> GetRolesAsync();

        Task<IdentityResult> AddToRoleAsync(string id, string roleName);

        Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName);
    }
}
