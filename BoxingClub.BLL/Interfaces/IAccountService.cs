using BoxingClub.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUp(IdentityUser user, string password);

        Task<bool> SignIn(UserDTO user);

        Task SignOut();

        Task<IdentityResult> CreateRole(RoleDTO role);

        IEnumerable<IdentityRole> GetRoles();

        Task<bool> IsInRole(IdentityUser user, string roleName);

        IEnumerable<IdentityUser> GetUsers();

        Task<IdentityRole> FindRoleById(string? id);

        Task<IdentityUser> FindUserById(string? id);

        Task<IdentityResult> AddToRole(IdentityUser user, string roleName);

        Task<IdentityResult> RemoveFromRole(IdentityUser user, string roleName);

        Task<IdentityResult> EditRole(IdentityRole role);

        Task Delete(string? id);
    }
}
