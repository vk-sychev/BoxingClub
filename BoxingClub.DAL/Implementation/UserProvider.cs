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
    class UserProvider : IUserProvider
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserProvider(UserManager<IdentityUser> userManager,
                            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.AddToRoleAsync(identityUser, roleName);
        }


        public async Task<IdentityUser> FindUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
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

        public async Task<IdentityResult> SignUpAsync(User user, string password, string roleName) //SignIn в другом сервисе делается
        {
            var identityUser = new IdentityUser(user.UserName);
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
