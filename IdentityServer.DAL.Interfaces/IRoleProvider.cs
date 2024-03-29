﻿using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.DAL.Interfaces
{
    public interface IRoleProvider
    {
        Task<ApplicationRole> FindRoleByIdAsync(string id);

        Task<ApplicationRole> FindRoleByNameAsync(string roleName);

        Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);

        Task<string> GetUserRole(ApplicationUser user);

        Task<List<ApplicationRole>> GetRolesAsync();

        Task<IdentityResult> AddToRoleAsync(string id, string roleName);

        Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName);
    }
}
