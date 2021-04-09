using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.DTO;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using BoxingClub.Infrastructure.HttpSwitcher;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoxingClub.WEB.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
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

        public async Task<IActionResult> GetAllStudents()
        {
            var studentDTOs = await _studentService.GetStudents();
            var students = _mapper.Map<List<StudentLiteViewModel>>(studentDTOs);
            return View(students);
        }

        public async Task<IActionResult> CreateStudent()
        {
            var groups = await _boxingGroupService.GetBoxingGroups();
            var model = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(model, "Id", "Name");
            ViewBag.Groups = selectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.CreateStudent(studentDTO);
                return RedirectToAction("GetAllStudents", "Student");
            }
            var groups = await _boxingGroupService.GetBoxingGroups();
            var groupViewModels = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(groupViewModels, "Id", "Name");
            ViewBag.Groups = selectList;
            return View(studentViewModel);

        }

        [Route("Student/DeleteStudent/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            await _studentService.DeleteStudent(id);
            return RedirectToAction("GetAllStudents", "Student");
        }

        [Route("Student/UpdateStudent/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateStudent(int? id, bool fromHomeController, int returnId)
        {
            var groups = await _boxingGroupService.GetBoxingGroups();
            var groupViewModels = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(groupViewModels, "Id", "Name");
            ViewBag.Groups = selectList;

            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            var studentDTO = await _studentService.GetStudent(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);

            return View(student);
        }

        [HttpPost]
        [Route("Student/UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(StudentFullViewModel studentViewModel, bool fromHomeController, int returnId)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.UpdateStudent(studentDTO);
                if (fromHomeController)
                {
                    return RedirectToAction("DetailsGroup", "Home", new { id = returnId });
                }
                return RedirectToAction("GetAllStudents", "Student");
            }
            var groups = await _boxingGroupService.GetBoxingGroups();
            var groupViewModels = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(groupViewModels, "Id", "Name");
            ViewBag.Groups = selectList;
            ViewBag.fromHomeController = fromHomeController;

            return View(studentViewModel);
        }

        [HttpGet]
        [Route("Student/DetailsStudent/{id}")]
        public async Task<IActionResult> DetailsStudent(int? id, bool fromHomeController, int returnId)
        {
            var studentDTO = await _studentService.GetStudent(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);
            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            return View(student);
        }
    }
}
