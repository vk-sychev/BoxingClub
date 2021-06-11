using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.BLL.Entities;
using IdentityServer.BLL.Interfaces;
using IdentityServer.Models;
using IdentityServer.WebManagers.Interfaces;

namespace IdentityServer.WebManagers.Implementation
{
    public class AdministrationWebManager : IAdministrationWebManager
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdministrationWebManager(IUserService userService,
                                        IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<PageViewModel<UserViewModel>> GetUsersAsync(SearchModelDTO searchModel)
        {
            var pageModel = await _userService.GetUsersPaginatedAsync(searchModel);
            var users = _mapper.Map<List<UserViewModel>>(pageModel.Items);
            return new PageViewModel<UserViewModel>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, users);
        }
    }
}
