using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class RoleProvider : IRoleProvider
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleProvider(RoleManager<IdentityRole> roleManager,
                            IMapper mapper)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(Role role)
        {
            var identityRole = _mapper.Map<IdentityRole>(role);
            return await _roleManager.CreateAsync(identityRole);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var role = _roleManager.FindByIdAsync(id);
            return await _roleManager.DeleteAsync(role.Result);
        }

        public async Task<IdentityResult> EditRoleAsync(Role role)
        {
            var identityRole = await _roleManager.FindByIdAsync(role.Id);
            identityRole.Name = role.Name;
            return await _roleManager.UpdateAsync(identityRole);
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
