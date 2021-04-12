using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Repositories
{
    public class AccountProvider : IAccountProvider
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountProvider(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager,
                               RoleManager<IdentityRole> roleManager,
                               IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.AddToRoleAsync(identityUser, roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(Role role)
        {
            var identityRole = _mapper.Map<IdentityRole>(role);
            return await _roleManager.CreateAsync(identityRole);
        }

        public async Task<IdentityResult> DeleteAsync(string id)
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

        public async Task<IdentityUser> FindUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var identityUser = _mapper.Map<IdentityUser>(user);
            var res = await _userManager.IsInRoleAsync(identityUser, roleName);
            return res;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.RemoveFromRoleAsync(identityUser, roleName);
        }

        public async Task<SignInResult> SignInAsync(User user)
        {
            return await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityRole> FindRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role;
        }

        public async Task<IdentityResult> SignUpAsync(User user, string password, string roleName)
        {
            var identityUser = new IdentityUser(user.UserName)
            {
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(identityUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, roleName);
                await _signInManager.SignInAsync(identityUser, isPersistent: false);
            }
            return result;
        }
    }
}
