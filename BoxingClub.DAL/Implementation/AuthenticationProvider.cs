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

        public async Task<SignInResult> SignInAsync(SignIn user) //Переименовать user в SignInModel
        {
            return await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
