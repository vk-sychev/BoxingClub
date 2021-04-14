using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class UserProvider : IUserProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserProvider(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.AddToRoleAsync(identityUser, roleName);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }


        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }


        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            //var identityUser = _mapper.Map<ApplicationUser>(user);
            var res = await _userManager.IsInRoleAsync(user, roleName);
            return res;
        }


        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.RemoveFromRoleAsync(identityUser, roleName);
        }

        public async Task<IdentityResult> SignUpAsync(ApplicationUser user, string password, string roleName)
        {
            var identityUser = new ApplicationUser();
            user.Id = identityUser.Id;
            
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roleName);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

    }
}
