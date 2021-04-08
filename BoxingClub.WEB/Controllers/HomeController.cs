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
            var groups = await _boxingGroupService.GetBoxingGroups();
            var model = _mapper.Map<List<BoxingGroupFullViewModel>>(groups);
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Home/EditGroup/{id}")]
        public async Task<IActionResult> EditGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroup(id);
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(group);

            var coaches = await _coachService.GetCoaches();
            var coacheViewModels = _mapper.Map<List<CoachViewModel>>(coaches);
            var selectList = new SelectList(coacheViewModels, "Id", "FIO");
            ViewBag.Coaches = selectList;

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
                await _boxingGroupService.UpdateGroup(group);
                return RedirectToAction("index", "home");
            }
            var coaches = await _coachService.GetCoaches();
            var coacheViewModels = _mapper.Map<List<CoachViewModel>>(coaches);
            var selectList = new SelectList(coacheViewModels, "Id", "FIO");
            ViewBag.Coaches = selectList;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Home/EditStudentsInGroup/{id}")]
        public async Task<IActionResult> EditStudentsInGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroup(id);

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateGroup()
        {
            var coaches = await _coachService.GetCoaches();
            var coacheViewModels = _mapper.Map<List<CoachViewModel>>(coaches);
            var selectList = new SelectList(coacheViewModels, "Id", "FIO");
            ViewBag.Coaches = selectList;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDTO = _mapper.Map<BoxingGroupDTO>(model);
                await _boxingGroupService.CreateGroup(groupDTO);
                return RedirectToAction("Index", "Home");
            }
            var coaches = await _coachService.GetCoaches();
            var coacheViewModels = _mapper.Map<List<CoachViewModel>>(coaches);
            var selectList = new SelectList(coacheViewModels, "Id", "FIO");
            ViewBag.Coaches = selectList;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("Home/DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(int? id)
        {
            await _boxingGroupService.DeleleGroup(id);
            return RedirectToAction("Index", "Home");
        }

        [Route("Home/DetailsGroup/{id}")]
        [HttpGet]
        public async Task<IActionResult> DetailsGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroupWithStudents(id);
            var model = _mapper.Map<BoxingGroupFullViewModel>(group);
            return View(model);
        }

        [Route("Home/DeleteFromGroup/{id}")]
        public async Task<IActionResult> DeleteFromGroup(int? id, int? returnId)
        {
            await _studentService.DeleteFromGroup(id);
            return RedirectToAction("DetailsGroup", new { id = returnId.Value });
        }
    }
}
