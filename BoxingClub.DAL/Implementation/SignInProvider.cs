using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class SignInProvider : ISignInProvider
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignInProvider(SignInManager<IdentityUser> signInManager,
                              IMapper mapper)
        {
            _signInManager = signInManager;
        }


        public async Task<SignInResult> SignInAsync(User user)
        {
            return await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
