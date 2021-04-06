using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IdentityResult> AddToRole(User user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.AddToRoleAsync(identityUser, roleName);
        }

        public async Task<IdentityResult> CreateRole(Role role)
        {
            var identityRole = _mapper.Map<IdentityRole>(role);
            return await _roleManager.CreateAsync(identityRole);
        }

        public async Task<IdentityResult> Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id);
            return await _roleManager.DeleteAsync(role.Result);
        }

        public async Task<IdentityResult> EditRole(Role role)
        {
            var identityRole = await _roleManager.FindByIdAsync(role.Id);
            identityRole.Name = role.Name;
            return await _roleManager.UpdateAsync(identityRole);
        }

        public async Task<IdentityRole> FindRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityUser> FindUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public List<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public List<IdentityUser> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<bool> IsInRole(User user, string roleName)
        {
            var identityUser = _mapper.Map<IdentityUser>(user);
            var res = await _userManager.IsInRoleAsync(identityUser, roleName);
            return res;
        }

        public async Task<IdentityResult> RemoveFromRole(User user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.RemoveFromRoleAsync(identityUser, roleName);
        }

        public async Task<SignInResult> SignIn(User user)
        {
            return await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> SignUp(User user, string password)
        {
            var identityUser = new IdentityUser(user.UserName);
            var result = await _userManager.CreateAsync(identityUser, password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("User");
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(identityUser, role.Name);
                }
                await _signInManager.SignInAsync(identityUser, isPersistent: false);
            }
            return result;
        }
    }
}
