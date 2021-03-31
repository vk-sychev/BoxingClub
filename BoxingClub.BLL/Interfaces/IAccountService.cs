using BoxingClub.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUp(IdentityUser user, string password);

        Task<bool> SignIn(userDTO user);

        Task SignOut();
    }
}
