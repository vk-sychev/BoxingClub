using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountProvider _accountProvider;
        private const string DefaultRoleName = "Manager";

        public AccountService(IMapper mapper,
                              IAccountProvider accountProvider)
        {
            _mapper = mapper;
            _accountProvider = accountProvider;
        }

        public async Task<AccountResultDTO> AddToRoleAsync(UserDTO userDTO, string roleName)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "User is null");
            }
            var user = _mapper.Map<User>(userDTO);
            var result = await _accountProvider.AddToRoleAsync(user, roleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> CreateRoleAsync(RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            var result = await _accountProvider.CreateRoleAsync(role);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> DeleteAsync(string id)
        {
            var result = await _accountProvider.DeleteRoleAsync(id);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> EditRoleAsync(RoleDTO role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role is null");
            }
            var result = await _accountProvider.EditRoleAsync(_mapper.Map<Role>(role));
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Role's id is null");
            }
            var role = await _accountProvider.FindRoleByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException($"Role with id = {id} isn't found", "");
            }
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<UserDTO> FindUserByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User's id is null");
            }
            var user = await _accountProvider.FindUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {id} isn't found", "");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            var roles = await _accountProvider.GetRolesAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _accountProvider.GetUsersAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }

            var result = await _accountProvider.IsInRoleAsync(_mapper.Map<User>(user), roleName);
            return result;
        }

        public async Task<AccountResultDTO> RemoveFromRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var result = await _accountProvider.RemoveFromRoleAsync(_mapper.Map<User>(user), roleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<SignInResultDTO> SignInAsync(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var result = await _accountProvider.SignInAsync(_mapper.Map<User>(user));
            return _mapper.Map<SignInResultDTO>(result);
        }

        public Task SignOutAsync()
        {
            return _accountProvider.SignOutAsync();
        }

        public async Task<AccountResultDTO> SignUpAsync(UserDTO user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var defaultRole = await _accountProvider.FindRoleByNameAsync(DefaultRoleName);
            if (defaultRole == null)
            {
                throw new InvalidOperationException($"Role with name {DefaultRoleName} doesn't exist");
            }
            var result = await _accountProvider.SignUpAsync(_mapper.Map<User>(user), password, DefaultRoleName);
            return _mapper.Map<AccountResultDTO>(result);
        }
    }
}
