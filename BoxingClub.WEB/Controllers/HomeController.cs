using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.Helpers;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.CustomAttributes;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BoxingClub.Web.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Index(SearchModelDTO searchModel)
        {
            if (User.IsInRole(Constants.UserRoleName))
            {
                return View("PendingRoleAssignment");
            }

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
            var response = await GetCoaches();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Users);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return View(mappedGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditBoxingGroupInline/{id}")]
        public async Task<IActionResult> EditBoxingGroupInline(int id)
        {
            var mappedGroup = await GetBoxingGroupById(id);

            var response = await GetCoaches();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Users);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

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

            var response = await GetCoaches();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Users);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/CreateBoxingGroup")]
        public async Task<IActionResult> CreateBoxingGroup()
        {
            var response = await GetCoaches();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Users);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/CreateBoxingGroupInline")]
        public async Task<IActionResult> CreateBoxingGroupInline()
        {
            var response = await GetCoaches();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Users);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

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

            var response = await GetCoaches();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Users);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [Route("Home/DetailsBoxingGroup/{id}")]
        [HttpGet]
        public async Task<IActionResult> DetailsBoxingGroup(int id)
        {
            var token = Request.Cookies["token"];
            var group = await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(id, token);
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

        private async Task<UsersResponseModel> GetCoaches()
        {
            var token = Request.Cookies["token"];
            return await _userClientAdapter.GetUsersByRole(token, Constants.CoachRoleName);
        }

        private SelectList GetCoachesSelectList(List<UserViewModel> coaches)
        {
            return new SelectList(coaches, "Id", "FullName");
        }

        private async Task<BoxingGroupLiteViewModel> GetBoxingGroupById(int id)
        {
            var token = Request.Cookies["token"];
            var group = await _boxingGroupService.GetBoxingGroupByIdAsync(id, token);
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(group);
            return mappedGroup;
        }
    }
}
