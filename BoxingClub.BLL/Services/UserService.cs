using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IUserProvider _userProvider;
        private readonly IMapper _mapper;
        private const string DefaultRoleName = "Manager";

        public UserService(IUserProvider userProvider,
                           IRoleProvider roleProvider,
                           IMapper mapper)
        {
            _userProvider = userProvider;
            _mapper = mapper;
            _roleProvider = roleProvider;
        }

        public async Task<UserDTO> FindUserByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User's id is null");
            }
            var user = await _userProvider.FindUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {id} isn't found", "");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _userProvider.GetUsersAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }

            var result = await _userProvider.IsInRoleAsync(_mapper.Map<User>(user), roleName);
            return result;
        }

        public async Task<AccountResultDTO> RemoveFromRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var result = await _userProvider.RemoveFromRoleAsync(_mapper.Map<User>(user), roleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> AddToRoleAsync(UserDTO userDTO, string roleName)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "User is null");
            }
            var user = _mapper.Map<User>(userDTO);
            var result = await _userProvider.AddToRoleAsync(user, roleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> SignUpAsync(UserDTO user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var defaultRole = await _roleProvider.FindRoleByNameAsync(DefaultRoleName);
            if (defaultRole == null)
            {
                throw new InvalidOperationException($"Role with name {DefaultRoleName} doesn't exist");
            }
            var result = await _userProvider.SignUpAsync(_mapper.Map<User>(user), password, DefaultRoleName);
            return _mapper.Map<AccountResultDTO>(result);
        }
    }
}
