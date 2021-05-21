using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class RoleProvider : IRoleProvider
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleProvider(RoleManager<ApplicationRole> roleManager,
                            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager), "roleManager is null");
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), "userManager is null");
        }

        public async Task<string> GetUserRole(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            return role;
        }

        public async Task<IdentityResult> AddToRoleAsync(string userId, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddToRoleAsync(identityUser, roleName);
        }


        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            return await _userManager.RemoveFromRoleAsync(identityUser, roleName);
        }

        public async Task<IdentityRole> FindRoleByIdAsync(string id)
        {
            /*return await _roleManager.FindByIdAsync(id);*/
            throw new NotImplementedException();
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            /*return await _roleManager.Roles.ToListAsync();*/
            throw new NotImplementedException();
        }

        public async Task<IdentityRole> FindRoleByNameAsync(string roleName)
        {
            /*return await _roleManager.FindByNameAsync(roleName);*/
            throw new NotImplementedException();
        }
    }
}
