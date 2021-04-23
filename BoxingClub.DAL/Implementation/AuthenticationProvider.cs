using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationProvider(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> SignInAsync(SignIn user)
        {
            return await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
