using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IUserProvider
    {
        Task<bool> IsInRoleAsync(User user, string roleName); 

        Task<IdentityResult> AddToRoleAsync(User user, string roleName);

        Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName); 

        Task<IdentityUser> FindUserByIdAsync(string id); 

        Task<List<IdentityUser>> GetUsersAsync();

        Task<IdentityResult> SignUpAsync(User user, string password, string roleName);
    }
}
