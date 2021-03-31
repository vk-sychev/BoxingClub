﻿using BoxingClub.WEB.Models;
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

namespace BoxingClub.WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;

        private readonly IMapper _mapper;

        public HomeController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<StudentLiteDTO> studentDTOs = await _studentService.GetStudents();
            var students = _mapper.Map<List<StudentLiteViewModel>>(studentDTOs);
            return View(students);
        }


        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.CreateStudent(studentDTO);
                return RedirectToAction("Index");
            }
            return View(studentViewModel);

        }

        [Route("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            await _studentService.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        [Route("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(int? id)
        {
            var studentDTO = await _studentService.GetStudent(id.Value);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);
            return View(student);
        }

        [HttpPost]
        [Route("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(StudentFullViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.UpdateStudent(studentDTO);
                return RedirectToAction("Index");
            }
            return View(studentViewModel);
        }


        /*        public IActionResult Privacy()
                {
                    return View();
                }*/

    }
}
