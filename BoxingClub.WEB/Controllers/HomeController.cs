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
using System.Linq;
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
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
            _boxingGroupService = boxingGroupService ?? throw new ArgumentNullException(nameof(boxingGroupService), "boxingGroupService is null");
            _userService = userService ?? throw new ArgumentNullException(nameof(userService), "userService is null");
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService), "studentService is null");
        }

        public async Task<IActionResult> Index(int? pageIndex, int? pageSize)
        {
            if (User.IsInRole(Constants.UserRoleName))
            {
                return View("PendingRoleAssignment");
            }

            List<BoxingGroupFullViewModel> groups;
            PageModelDTO<BoxingGroupDTO> pageModel;

            if (User.IsInRole(Constants.CoachRoleName))
            {
                var coach = await _userService.FindUserByNameAsync(User.Identity.Name);
                pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(coach.Id, pageIndex ?? 1, pageSize ?? 3);
                if (!pageModel.Items.Any())
                {
                    pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(coach.Id, 1, pageSize ?? 3);
                    pageIndex = 1;
                }
            }
            else
            {
                pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(pageIndex ?? 1, pageSize ?? 3);
                if (!pageModel.Items.Any())
                {
                    pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(1, pageSize ?? 3);
                    pageIndex = 1;
                }           
            }
            groups = _mapper.Map<List<BoxingGroupFullViewModel>>(pageModel.Items);
            var pageViewModel = new PageViewModel<BoxingGroupFullViewModel>(pageModel.Count, pageIndex ?? 1, pageSize ?? 3, groups);

            var sizes = new List<int> { 1, 2, 3, 4, 5 };
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = pageSize ?? 3;

            return View(pageViewModel);
        }



        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditGroup/{id}")]
        public async Task<IActionResult> EditBoxingGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupByIdAsync(id.Value);
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
            await _boxingGroupService.DeleleBoxingGroupAsync(id.Value);
            return RedirectToAction("Index", "Home");
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [Route("Home/DetailsGroup/{id}")]
        [HttpGet]
        public async Task<IActionResult> DetailsBoxingGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(id.Value);
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
