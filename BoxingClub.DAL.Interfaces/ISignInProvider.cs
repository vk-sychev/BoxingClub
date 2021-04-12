using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface ISignInProvider
    {
        Task<SignInResult> SignInAsync(User user);

        Task SignOutAsync();
    }
}
