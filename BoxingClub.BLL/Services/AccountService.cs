using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(IMapper mapper,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public Task<IdentityResult> AddToRole(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "Role is null");
            }
            return _userManager.AddToRoleAsync(user, roleName);
        }

        public Task<IdentityResult> CreateRole(RoleDTO role)
        {
            IdentityRole identityRole = new IdentityRole { Name = role.RoleName };
            return _roleManager.CreateAsync(identityRole);
        }

        public async Task Delete(string id)
        {
            var role = FindRoleById(id);
            await _roleManager.DeleteAsync(role.Result);
        }

        public Task<IdentityResult> EditRole(IdentityRole role)
        {
            if (role == null)
            {
                throw new NotFoundException(nameof(role), "Role is null");
            }

            var foundRole = FindRoleById(role.Id);
            foundRole.Result.Name = role.Name;

            return  _roleManager.UpdateAsync(foundRole.Result);
        }

        public Task<IdentityRole> FindRoleById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Role's id is null");
            }
            var role = _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException($"Role with id = {id} isn't found", "");
            }
            return role;
        }

        public Task<IdentityUser> FindUserById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User's id is null");
            }
            var user = _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {id} isn't found", "");
            }
            return user;
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles;
        }

        public IEnumerable<IdentityUser> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public Task<bool> IsInRole(IdentityUser user, string roleName)
        {
            var result = _userManager.IsInRoleAsync(user, roleName);
            return result;
        }

        public Task<IdentityResult> RemoveFromRole(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "Role is null");
            }
            return _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<bool> SignIn(UserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.NickName, user.Password, user.RememberMe, false);
            return result.Succeeded ? true : false;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> SignUp(IdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("user");
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }
    }
}
