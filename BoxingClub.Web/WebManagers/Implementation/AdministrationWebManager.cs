using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.WebManagers.Implementation
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
