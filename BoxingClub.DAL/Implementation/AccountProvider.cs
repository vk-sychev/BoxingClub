using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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


    }
}
