using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IMapper _mapper;

        public AuthenticationService(IAuthenticationProvider authenticationProvider,
                                     IMapper mapper)
        {
            _authenticationProvider = authenticationProvider;
            _mapper = mapper;
        }

        public async Task<SignInResultDTO> SignInAsync(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var mappedUser = _mapper.Map<SignIn>(user);
            var result = await _authenticationProvider.SignInAsync(mappedUser);
            return _mapper.Map<SignInResultDTO>(result);
        }

        public Task SignOutAsync()
        {
            return _authenticationProvider.SignOutAsync();
        }
    }
}
