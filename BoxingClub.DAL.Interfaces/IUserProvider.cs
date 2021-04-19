using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUserProvider
    {
        Task<bool> DeleteUserAsync(string id);

        Task<ApplicationUser> FindUserByIdAsync(string userId);

        Task<List<ApplicationUser>> GetUsersAsync();

        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName);

        Task<IdentityResult> SignUpAsync(ApplicationUser user, string password, string roleName);

        Task<ApplicationUser> GetUserByNameAsync(string name);
    }
}
