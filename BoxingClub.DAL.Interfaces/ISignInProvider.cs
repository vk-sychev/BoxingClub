﻿using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface ISignInProvider
    {
        Task<SignInResult> SignInAsync(SignIn user);

        Task SignOutAsync();
    }
}
