using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IRoleProvider
    {
        Task<IdentityResult> CreateRoleAsync(Role role);

        Task<IdentityResult> DeleteRoleAsync(string id);

        Task<IdentityResult> EditRoleAsync(Role role);

        Task<IdentityRole> FindRoleByIdAsync(string id);

        Task<IdentityRole> FindRoleByNameAsync(string roleName);

        Task<List<IdentityRole>> GetRolesAsync();
    }
}
