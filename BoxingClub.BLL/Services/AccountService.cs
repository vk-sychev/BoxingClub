using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(IMapper mapper,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> SignIn(userDTO user)
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
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }
    }
}
