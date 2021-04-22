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
        private readonly IMapper _mapper;

        public UserProvider(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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


        public Task<List<ApplicationUser>> GetUsersAsync()
        {
            return _userManager.Users.ToListAsync();
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

        public Task<IList<ApplicationUser>> GetUsersByRoleAsync(string roleName)
        {
            return _userManager.GetUsersInRoleAsync(roleName);
        }

        public Task<ApplicationUser> GetUserByNameAsync(string name)
        {
            return _userManager.FindByNameAsync(name);
        }
    }
}
