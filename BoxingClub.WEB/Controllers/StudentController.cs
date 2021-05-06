﻿using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.DomainEntities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.CustomAttributes;
using System.Linq;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.Models.Enums;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService,
                                 IMapper mapper,
                                 IBoxingGroupService boxingGroupService)
        {
            _studentService = studentService;
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        public async Task<IActionResult> GetAllStudents(int? pageIndex, int? pageSize, int? filter)
        {
            var searchModel = new SearchModelDTO()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Filter = filter
            };

            var pageModel = await _studentService.GetStudentsAsync(searchModel);
            pageIndex = searchModel.PageIndex;

            var students = _mapper.Map<List<StudentLiteViewModel>>(pageModel.Items);
            var pageViewModel = new PageViewModel<StudentLiteViewModel>(pageModel.Count, pageIndex, pageSize, students);

            var sizes = new List<int> { 1, 2, 3, 4, 5 }; //в конфиг
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = pageSize ?? 3;
            ViewBag.filter = filter ?? 0;

            return View(pageViewModel);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        public async Task<IActionResult> CreateStudent()
        {
            ViewBag.Groups = await GetGroups();
            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.CreateStudentAsync(studentDTO);
                return RedirectToAction("GetAllStudents", "Student");
            }
            ViewBag.Groups = await GetGroups();
            return View(studentViewModel);

        }

        [Route("Student/DeleteStudent/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName)]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("GetAllStudents", "Student");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Student/EditStudent/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditStudent(int? id, bool fromHomeController, int returnId)
        {

            ViewBag.Groups = await GetGroups();
            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            var studentDTO = await _studentService.GetStudentByIdAsync(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);

            return View(student);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost]
        [Route("Student/EditStudent/{id}")]
        public async Task<IActionResult> EditStudent(StudentFullViewModel studentViewModel, bool fromHomeController, int returnId)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.UpdateStudentAsync(studentDTO);
                if (fromHomeController)
                {
                    return RedirectToAction("DetailsGroup", "Home", new { id = returnId });
                }
                return RedirectToAction("GetAllStudents", "Student");
            }
            ViewBag.Groups = await GetGroups();
            ViewBag.fromHomeController = fromHomeController;

            return View(studentViewModel);
        }

        [HttpGet]
        [Route("Student/DetailsStudent/{id}")]
        public async Task<IActionResult> DetailsStudent(int? id, bool fromHomeController, int returnId)
        {
            var studentDTO = await _studentService.GetStudentByIdAsync(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);
            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            return View(student);
        }


        private async Task<SelectList> GetGroups()
        {
            var groups = await _boxingGroupService.GetBoxingGroupsAsync();
            var groupViewModels = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(groupViewModels, "Id", "Name");
            return selectList;
        }
    }
}
