using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Students.API.Models;
using Students.API.WebManagers.Interfaces;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces;

namespace Students.API.WebManagers.Implementation
{
    public class HomeWebManager : IHomeWebManager
    {
        private readonly IBoxingGroupService _boxingGroupService;

        public HomeWebManager(IBoxingGroupService boxingGroupService)
        {
            _boxingGroupService = boxingGroupService;
        }

        public async Task<PageViewModel<BoxingGroupDTO>> GetBoxingGroupsAsync(SearchModelDTO searchModel, string token)
        {
            var pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(searchModel, token);
            return new PageViewModel<BoxingGroupDTO>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, pageModel.Items);
        }

        public async Task<PageViewModel<BoxingGroupDTO>> GetBoxingGroupsByCoachIdAsync(string coachName, SearchModelDTO searchModel, string token)
        {
            var pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(coachName, searchModel, token);
            return new PageViewModel<BoxingGroupDTO>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, pageModel.Items);
        }
    }
}
