using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IAccountProvider
    {
        Task<IdentityResult> CreateRoleAsync(Role role);

        Task<IdentityResult> DeleteAsync(string id);

        Task<IdentityResult> EditRoleAsync(Role role);

        Task<IdentityRole> FindRoleByIdAsync(string id);

        Task<IdentityRole> FindRoleByNameAsync(string roleName);

        Task<List<IdentityRole>> GetRolesAsync();

        Task<bool> IsInRoleAsync(User user, string roleName);



        Task<IdentityResult> AddToRoleAsync(User user, string roleName);

        Task<IdentityUser> FindUserByIdAsync(string id);

        Task<List<IdentityUser>> GetUsersAsync();

        

        Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);



        Task<SignInResult> SignInAsync(User user);

        Task SignOutAsync();

        Task<IdentityResult> SignUpAsync(User user, string password, string roleName);
    }
}