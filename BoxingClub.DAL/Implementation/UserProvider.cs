using AutoMapper;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Implementation.Implementation
{
    public class UserProvider : IUserProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserProvider(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.GetRolesAsync(user);
            return user;
        }


        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }


        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, string roleName)
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

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }
    }
}
