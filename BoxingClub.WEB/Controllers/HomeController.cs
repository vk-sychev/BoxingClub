using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;

        public HomeController(IMapper mapper,
                              IBoxingGroupService boxingGroupService,
                              IUserService userService,
                              IStudentService studentService)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _userService = userService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index(int? pageIndex)
        {
            if (User.IsInRole(Constants.UserRoleName))
            {
                return View("PendingRoleAssignment");
            }

            List<BoxingGroupDTO> groups;
            int pageSize = 1;
            //PageViewModel<BoxingGroupFullViewModel> pageViewModel;

            /*            if (User.IsInRole(Constants.CoachRoleName))
                        {
                            var coach = await _userService.FindUserByNameAsync(User.Identity.Name);
                            groups = await _boxingGroupService.GetBoxingGroupsByCoachIdAsync(coach.Id);
                        }
                        else*/
            //{
            var pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(pageIndex ?? 1, pageSize);
            var mappedItems = _mapper.Map<List<BoxingGroupFullViewModel>>(pageModel.Items);
            var pageViewModel = new PageViewModel<BoxingGroupFullViewModel>(pageModel.Count, pageIndex ?? 1, pageSize, mappedItems);
            //pageViewModel = _mapper.Map<PageViewModel<BoxingGroupFullViewModel>>(pageModel);


            //}

            return View(pageViewModel);
        }


        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditGroup/{id}")]
        public async Task<IActionResult> EditBoxingGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupByIdAsync(id);
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(group);

            ViewBag.Coaches = await GetCoaches();

            return View(mappedGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost]
        [Route("Home/EditGroup/{id}")]
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
        public async Task<IActionResult> CreateBoxingGroup()
        {
            ViewBag.Coaches = await GetCoaches();
            return View();
        }

        private async Task<SelectList> GetCoaches()
        {
            var coaches = await _userService.GetUsersByRoleAsync(Constants.CoachRoleName);
            var coacheViewModels = _mapper.Map<List<UserViewModel>>(coaches);
            var selectList = new SelectList(coacheViewModels, "Id", "FullName");
            return selectList;
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost]
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

        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Home/DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteBoxingGroup(int? id)
        {
            await _boxingGroupService.DeleleBoxingGroupAsync(id);
            return RedirectToAction("Index", "Home");
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [Route("Home/DetailsGroup/{id}")]
        [HttpGet]
        public async Task<IActionResult> DetailsBoxingGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(id);
            var model = _mapper.Map<BoxingGroupFullViewModel>(group);
            return View(model);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Home/DeleteFromGroup/{studentId}")]
        public async Task<IActionResult> DeleteFromBoxingGroup(int? studentId, int? returnId)
        {
            await _studentService.DeleteFromGroupAsync(studentId);
            return RedirectToAction("DetailsGroup", new { id = returnId.Value });
        }
    }
}
