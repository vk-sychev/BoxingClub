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
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;

        public HomeWebManager(IMapper mapper,
                              IBoxingGroupService boxingGroupService)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
        }

        public async Task<PageViewModel<BoxingGroupLiteViewModel>> GetBoxingGroupsAsync(SearchModelDTO searchModel, string token)
        {
            var pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(searchModel, token);
            var groups = _mapper.Map<List<BoxingGroupLiteViewModel>>(pageModel.Items);
            return new PageViewModel<BoxingGroupLiteViewModel>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, groups);
        }

        public async Task<PageViewModel<BoxingGroupLiteViewModel>> GetBoxingGroupsByCoachIdAsync(string coachName, SearchModelDTO searchModel, string token)
        {
            var pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(coachName, searchModel, token);
            var groups = _mapper.Map<List<BoxingGroupLiteViewModel>>(pageModel.Items);
            return new PageViewModel<BoxingGroupLiteViewModel>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, groups);
        }
    }
}
