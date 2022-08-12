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

        [HttpGet("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.CoachRoleName, Constants.ManagerRoleName)]
        public async Task<IActionResult> GetBoxingGroups(SearchModelDTO searchModel)
        {
            PageViewModel<BoxingGroupDTO> pageViewModel;
            var token = GetTokenFromRequest();

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

        [HttpGet("[action]")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.CoachRoleName, Constants.ManagerRoleName)]
        public async Task<IActionResult> GetAllBoxingGroups()
        {
            var token = GetTokenFromRequest();
            var boxingGroups = await _boxingGroupService.GetBoxingGroupsAsync(token);
            return Ok(boxingGroups);
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> GetBoxingGroup(int id)
        {
            var mappedGroup = await GetBoxingGroupById(id);
            return Ok(mappedGroup);
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> GetBoxingGroupWithStudents(int id)
        {
            var token = GetTokenFromRequest();
            var boxingGroup = await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(id, token);
            return Ok(boxingGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBoxingGroup(BoxingGroupDTO model)
        {
            if (ModelState.IsValid)
            {
                var groupDTO = _mapper.Map<BoxingGroupDTO>(model);
                await _boxingGroupService.CreateBoxingGroupAsync(groupDTO);
                return Ok();
            }

            return BadRequest();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteBoxingGroup(int id)
        {
            await _boxingGroupService.DeleteBoxingGroupAsync(id);
            return Ok();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpDelete("[action]/{studentId}")]
        public async Task<IActionResult> DeleteFromBoxingGroup(int studentId)
        {
            await _studentService.DeleteFromGroupAsync(studentId);
            return Ok();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> EditBoxingGroup(BoxingGroupDTO model)
        {
            if (ModelState.IsValid)
            {
                await _boxingGroupService.UpdateBoxingGroupAsync(model);
                return Ok();
            }

            return BadRequest();
        }

        private async Task<BoxingGroupDTO> GetBoxingGroupById(int id)
        {
            var token = GetTokenFromRequest();
            var group = await _boxingGroupService.GetBoxingGroupByIdAsync(id, token);
            return group;
        }

        private string GetTokenFromRequest()
        {
            var header = Request.Headers["Authorization"];
            var token = header.ToString().Split(' ');
            return token[1];
        }
    }
}
