using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
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

        public AccountService(IMapper mapper,
                              IAccountProvider accountProvider)
        {
            _mapper = mapper;
            _accountProvider = accountProvider;
        }

        public async Task<AccountResultDTO> AddToRole(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var result = await _accountProvider.AddToRole(_mapper.Map<User>(user), roleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> CreateRole(RoleDTO role)
        {
            var result = await _accountProvider.CreateRole(_mapper.Map<Role>(role));
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> Delete(string id)
        {
            return _mapper.Map<AccountResultDTO>(await _accountProvider.Delete(id));
        }

        public async Task<AccountResultDTO> EditRole(RoleDTO role)
        {
            if (role == null)
            {
                throw new NotFoundException(nameof(role), "Role is null");
            }
            return _mapper.Map<AccountResultDTO>(await _accountProvider.EditRole(_mapper.Map<Role>(role)));
        }

        public async Task<RoleDTO> FindRoleById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Role's id is null");
            }
            var role = await _accountProvider.FindRoleById(id);
            if (role == null)
            {
                throw new NotFoundException($"Role with id = {id} isn't found", "");
            }
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<UserDTO> FindUserById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User's id is null");
            }
            var user = await _accountProvider.FindUserById(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {id} isn't found", "");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            return _mapper.Map<List<RoleDTO>>(await _accountProvider.GetRoles());
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return _mapper.Map<List<UserDTO>>(await _accountProvider.GetUsers());
        }

        public async Task<bool> IsInRole(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "User is null");
            }

            return await _accountProvider.IsInRole(_mapper.Map<User>(user), roleName);
        }

        public async Task<AccountResultDTO> RemoveFromRole(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "User is null");
            }
            return _mapper.Map<AccountResultDTO>(await _accountProvider.RemoveFromRole(_mapper.Map<User>(user), roleName));
        }

        public async Task<SignInResultDTO> SignIn(UserDTO user)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "User is null");
            }
            return _mapper.Map<SignInResultDTO>(await _accountProvider.SignIn(_mapper.Map<User>(user)));
        }

        public Task SignOut()
        {
            return _accountProvider.SignOut();
        }

        public async Task<AccountResultDTO> SignUp(UserDTO user, string password)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "User is null");
            }
            return _mapper.Map<AccountResultDTO>(await _accountProvider.SignUp(_mapper.Map<User>(user), password));
        }
    }
}
