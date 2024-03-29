﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.DAL.Entities;
using IdentityServer.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace IdentityServer.DAL.Implementation.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProvider(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), "userManager is null");
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            var user = await _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Id.Equals(id));
            return user;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            var identityUser = new ApplicationUser();
            user.Id = identityUser.Id;

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {   
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<List<ApplicationUser>> GetUsersPaginatedAsync(int pageIndex, int pageSize)
        {
            var query = _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role);
            var list = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<int> GetCountOfUsersAsync()
        {
            var query = _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role);
            var count = await query.CountAsync();
            return count;
        }

        public bool IsSupportUserRole()
        {
            return _userManager.SupportsUserRole;
        }
    }
}
