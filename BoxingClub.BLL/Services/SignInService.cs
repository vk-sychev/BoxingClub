using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Implementation.Services
{
    public class SignInService : ISignInService
    {
        private readonly ISignInProvider _signInProvider;
        private readonly IMapper _mapper;

        public SignInService(ISignInProvider signInProvider,
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
