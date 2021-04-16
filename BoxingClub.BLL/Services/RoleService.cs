using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IMapper _mapper;

        public RoleService(IRoleProvider roleProvider,
                           IMapper mapper)
        {
            _roleProvider = roleProvider;
            _mapper = mapper;
        }


        public async Task<AccountResultDTO> CreateRoleAsync(RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            var result = await _roleProvider.CreateRoleAsync(role);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> DeleteRoleAsync(string id)
        {
            var result = await _roleProvider.DeleteRoleAsync(id);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> EditRoleAsync(RoleDTO role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role is null");
            }
            var result = await _roleProvider.EditRoleAsync(_mapper.Map<Role>(role));
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Role's id is null");
            }
            var role = await _roleProvider.FindRoleByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException($"Role with id = {id} isn't found", "");
            }
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            var roles = await _roleProvider.GetRolesAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        }
    }
}
