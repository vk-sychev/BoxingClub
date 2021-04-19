using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Controllers
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

        public async Task<IActionResult> Index()
        {
            List<BoxingGroupDTO> groups;
            if (User.IsInRole("User"))
            {
                return View("PendingRoleAssignment");
            }

            if (User.IsInRole("Coach"))
            {
                var coach = await _userService.FindUserByNameAsync(User.Identity.Name);
                groups = await _boxingGroupService.GetBoxingGroupsByCoachIdAsync(coach.Id);
            }
            else
            {
                groups = await _boxingGroupService.GetBoxingGroupsAsync();
            }
            var model = _mapper.Map<List<BoxingGroupFullViewModel>>(groups);
            return View(model);
        }


        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditGroup/{id}")]
        public async Task<IActionResult> EditBoxingGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupAsync(id);
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
                return RedirectToAction("index", "home");
            }
            ViewBag.Coaches = await GetCoaches();

            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet]
        [Route("Home/EditStudentsInGroup/{id}")]
        public async Task<IActionResult> EditStudentsInBoxingGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupAsync(id);
            return View();
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
            var coaches = await _userService.GetUsersByRoleAsync("Coach");
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
            var group = await _boxingGroupService.GetBoxingGroupWithStudentsAsync(id);
            var model = _mapper.Map<BoxingGroupFullViewModel>(group);
            return View(model);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Home/DeleteFromGroup/{id}")]
        public async Task<IActionResult> DeleteFromBoxingGroup(int? id, int? returnId)
        {
            await _studentService.DeleteFromGroupAsync(id);
            return RedirectToAction("DetailsGroup", new { id = returnId.Value });
        }
    }
}
