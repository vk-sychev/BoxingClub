using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.CustomAttributes;
using BoxingClub.Infrastructure.Helpers;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Students.API.Models;
using Students.API.WebManagers.Interfaces;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces;

namespace Students.API.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IStudentService _studentService;
        private readonly IHomeWebManager _homeWebManager;
        private readonly IUserClientAdapter _userClientAdapter;

        public HomeController(IMapper mapper,
            IBoxingGroupService boxingGroupService,
            IStudentService studentService,
            IHomeWebManager homeWebManager,
            IUserClientAdapter userClientAdapter)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _studentService = studentService;
            _homeWebManager = homeWebManager;
            _userClientAdapter = userClientAdapter;
        }

        [Route("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.CoachRoleName, Constants.ManagerRoleName)]
        public async Task<IActionResult> GetBoxingGroups(SearchModelDTO searchModel)
        {
            PageViewModel<BoxingGroupLiteViewModel> pageViewModel;
            var token = Request.Cookies["token"];

            if (User.IsInRole(Constants.CoachRoleName))
            {
                pageViewModel = await _homeWebManager.GetBoxingGroupsByCoachIdAsync(User.Identity.Name, searchModel, token);
            }
            else
            {
                pageViewModel = await _homeWebManager.GetBoxingGroupsAsync(searchModel, token);
            }

            return Ok(pageViewModel);
        }

        [Route("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> EditBoxingGroup(int id)
        {
            var mappedGroup = await GetBoxingGroupById(id);
            return Ok(mappedGroup);
        }

        private async Task<BoxingGroupLiteViewModel> GetBoxingGroupById(int id)
        {
            var token = GetTokenFromRequest();
            var group = await _boxingGroupService.GetBoxingGroupByIdAsync(id, token);
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(group);
            return mappedGroup;
        }

        private string GetTokenFromRequest()
        {
            var header = Request.Headers["Authorization"];
            var token = header.ToString().Split(' ');
            return token[1];
        }
    }
}
