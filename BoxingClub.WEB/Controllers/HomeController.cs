using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Helpers;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BoxingClub.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly IHomeWebManager _homeWebManager;

        public HomeController(IMapper mapper,
                              IBoxingGroupService boxingGroupService,
                              IUserService userService,
                              IStudentService studentService,
                              IHomeWebManager homeWebManager)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _userService = userService;
            _studentService = studentService;
            _homeWebManager = homeWebManager;
        }

        public async Task<IActionResult> Index(SearchModelDTO searchModel)
        {
            if (User.IsInRole(Constants.UserRoleName))
            {
                return View("PendingRoleAssignment");
            }

            PageViewModel<BoxingGroupLiteViewModel> pageViewModel;

            if (User.IsInRole(Constants.CoachRoleName))
            {
                pageViewModel = await _homeWebManager.GetBoxingGroupsByCoachIdAsync(User.Identity.Name, searchModel);
            }
            else
            {
                pageViewModel = await _homeWebManager.GetBoxingGroupsAsync(searchModel);
            }

            var sizes = PageSizeHelper.GetPageSizeList(5);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = searchModel.PageSize;

            return View(pageViewModel);
        }


        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditBoxingGroup/{id}")]
        public async Task<IActionResult> EditBoxingGroup(int id)
        {
            var mappedGroup = await GetBoxingGroupById(id);

            ViewBag.Coaches = await GetCoaches();

            return View(mappedGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditBoxingGroupInline/{id}")]
        public async Task<IActionResult> EditBoxingGroupInline(int id)
        {
            var mappedGroup = await GetBoxingGroupById(id);

            ViewBag.Coaches = await GetCoaches();

            return PartialView("_CreateEditBoxingGroupPartial", mappedGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost]
        [Route("Home/EditBoxingGroup/{id}")]
        public async Task<IActionResult> EditBoxingGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _mapper.Map<BoxingGroupDTO>(model);
                await _boxingGroupService.UpdateBoxingGroupAsync(group);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Coaches = await GetCoaches();

            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/CreateBoxingGroup")]
        public async Task<IActionResult> CreateBoxingGroup()
        {
            ViewBag.Coaches = await GetCoaches();
            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/CreateBoxingGroupInline")]
        public async Task<IActionResult> CreateBoxingGroupInline()
        {
            ViewBag.Coaches = await GetCoaches();
            return PartialView("_CreateEditBoxingGroupPartial");
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost]
        [Route("Home/CreateBoxingGroup")]
        public async Task<IActionResult> CreateBoxingGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDTO = _mapper.Map<BoxingGroupDTO>(model);
                await _boxingGroupService.CreateBoxingGroupAsync(groupDTO);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Coaches = await GetCoaches();
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [Route("Home/DetailsBoxingGroup/{id}")]
        [HttpGet]
        public async Task<IActionResult> DetailsBoxingGroup(int id)
        {
            var group = await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(id);
            var model = _mapper.Map<BoxingGroupFullViewModel>(group);
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Home/DeleteBoxingGroup/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoxingGroup(int id)
        {
            await _boxingGroupService.DeleteBoxingGroupAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Home/DetailsGroup/DeleteFromBoxingGroup/{studentId}")]
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteFromBoxingGroup(int studentId, int returnId)
        {
            await _studentService.DeleteFromGroupAsync(studentId);
            return RedirectToAction("DetailsBoxingGroup", new { id = returnId });
        }

        private async Task<SelectList> GetCoaches()
        {
            var coaches = await _userService.GetUsersByRoleAsync(Constants.CoachRoleName);
            var coachViewModels = _mapper.Map<List<UserViewModel>>(coaches);
            var selectList = new SelectList(coachViewModels, "Id", "FullName");
            return selectList;
        }

        private async Task<BoxingGroupLiteViewModel> GetBoxingGroupById(int id)
        {
            var group = await _boxingGroupService.GetBoxingGroupByIdAsync(id);
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(group);
            return mappedGroup;
        }
    }
}
