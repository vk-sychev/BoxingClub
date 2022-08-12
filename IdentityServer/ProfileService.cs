using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.BLL.Interfaces;
using IdentityServer.DAL.Entities;
using IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;

        public ProfileService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userService.FindUserByIdAsync(userId);
            var claims = await _userService.GetUserClaims(userId);
            claims = claims.Where(c => context.RequestedClaimTypes.Contains(c.Type)).ToList();

            if (_userService.IsSupportUserRole())
            {
                var userRole = user.Role.Name;
                if (!string.IsNullOrEmpty(userRole))
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            context.IssuedClaims = claims;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
