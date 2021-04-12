using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly ICoachService _coachService;
        private readonly IStudentService _studentService;

        public HomeController(IMapper mapper,
                              IBoxingGroupService boxingGroupService,
                              ICoachService coachService,
                              IStudentService studentService)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _coachService = coachService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _boxingGroupService.GetBoxingGroupsAsync();
            var model = _mapper.Map<List<BoxingGroupFullViewModel>>(groups);
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Home/EditGroup/{id}")]
        public async Task<IActionResult> EditGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupAsync(id);
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(group);

            ViewBag.Coaches = await GetCoaches();

            return View(mappedGroup);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Home/EditGroup/{id}")]
        public async Task<IActionResult> EditGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _mapper.Map<BoxingGroupDTO>(model);
                await _boxingGroupService.UpdateGroupAsync(group);
                return RedirectToAction("index", "home");
            }
            ViewBag.Coaches = await GetCoaches();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Home/EditStudentsInGroup/{id}")]
        public async Task<IActionResult> EditStudentsInGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupAsync(id);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateGroup()
        {
            ViewBag.Coaches = await GetCoaches();
            return View();
        }

        private async Task<SelectList> GetCoaches()
        {
            var coaches = await _coachService.GetCoachesAsync();
            var coacheViewModels = _mapper.Map<List<CoachViewModel>>(coaches);
            var selectList = new SelectList(coacheViewModels, "Id", "FullName");
            return selectList;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDTO = _mapper.Map<BoxingGroupDTO>(model);
                await _boxingGroupService.CreateGroupAsync(groupDTO);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Coaches = await GetCoaches();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("Home/DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(int? id)
        {
            await _boxingGroupService.DeleleGroupAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [Route("Home/DetailsGroup/{id}")]
        [HttpGet]
        public async Task<IActionResult> DetailsGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupWithStudentsAsync(id);
            var model = _mapper.Map<BoxingGroupFullViewModel>(group);
            return View(model);
        }

        [Route("Home/DeleteFromGroup/{id}")]
        public async Task<IActionResult> DeleteFromGroup(int? id, int? returnId)
        {
            await _studentService.DeleteFromGroupAsync(id);
            return RedirectToAction("DetailsGroup", new { id = returnId.Value });
        }
    }
}
