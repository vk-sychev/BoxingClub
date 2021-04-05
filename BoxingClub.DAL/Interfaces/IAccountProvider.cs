using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IAccountProvider
    {
        Task<IdentityResult> AddToRole(User user, string roleName);
        Task<IdentityResult> CreateRole(Role role);
        Task<IdentityResult> Delete(string id);
        Task<IdentityResult> EditRole(Role role);
        Task<IdentityRole> FindRoleById(string id);
        Task<IdentityUser> FindUserById(string id);
        List<IdentityRole> GetRoles();
        List<IdentityUser> GetUsers();
        Task<bool> IsInRole(User user, string roleName);
        Task<IdentityResult> RemoveFromRole(User user, string roleName);
        Task<SignInResult> SignIn(User user);
        Task SignOut();
        Task<IdentityResult> SignUp(User user, string password);
    }
}