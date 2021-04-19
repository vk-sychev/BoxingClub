﻿using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class SignInService : IAuthenticationService
    {
        private readonly IAuthenticationProvider _signInProvider;
        private readonly IMapper _mapper;

        public SignInService(IAuthenticationProvider signInProvider,
                             IMapper mapper)
        {
            _signInProvider = signInProvider;
            _mapper = mapper;
        }

        public async Task<SignInResultDTO> SignInAsync(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var result = await _signInProvider.SignInAsync(_mapper.Map<SignIn>(user));
            return _mapper.Map<SignInResultDTO>(result);
        }

        public Task SignOutAsync()
        {
            return _signInProvider.SignOutAsync();
        }
    }
}
