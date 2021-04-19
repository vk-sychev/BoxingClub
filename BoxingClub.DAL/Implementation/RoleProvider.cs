using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class RoleProvider : IRoleProvider
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleProvider(RoleManager<IdentityRole> roleManager,
                            UserManager<ApplicationUser> userManager,
                            IMapper mapper)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
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
            var res = await _userManager.IsInRoleAsync(user, roleName);
            return res;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            return await _userManager.RemoveFromRoleAsync(identityUser, roleName);
        }

        public async Task<IdentityRole> FindRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole> FindRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role;
        }
    }
}
