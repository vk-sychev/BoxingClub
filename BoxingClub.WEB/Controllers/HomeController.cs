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

namespace BoxingClub.WEB.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IStudentService _studentService;

        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IStudentService studentService, IMapper mapper)
        {
            _logger = logger;
            _studentService = studentService;
            _mapper = mapper;
        }

        
        public async Task<IActionResult> Index()
        {
            IEnumerable<StudentLiteDTO> studentDTOs = await _studentService.GetStudents();
            var students = _mapper.Map<IEnumerable<StudentLiteDTO>, List<StudentLiteViewModel>>(studentDTOs);
            return View(students);
        }


        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
            await _studentService.CreateStudent(studentDTO);
            return RedirectToAction("Index");

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
        public async Task<IActionResult> UpdateStudent(StudentFullViewModel studentViewModel)
        {
            var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
            await _studentService.UpdateStudent(studentDTO);
            return RedirectToAction("Index");
        }


        /*        public IActionResult Privacy()
                {
                    return View();
                }*/




        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                Message = exceptionDetails.Error.Message,
                StatusCode = HttpSwitch.SwitchHttpCode(exceptionDetails.Error.GetType())
            };
            _logger.LogError(exceptionDetails.Error.Message);
            return View(errorViewModel);
        }
    }
}
