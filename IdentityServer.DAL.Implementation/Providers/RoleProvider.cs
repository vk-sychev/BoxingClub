﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.DAL.Entities;
using IdentityServer.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace IdentityServer.DAL.Implementation.Providers
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

        public async Task<ApplicationRole> FindRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<List<ApplicationRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> FindRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}
