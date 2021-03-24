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

        public IActionResult Index()
        {
            try
            {
                IEnumerable<StudentLiteDTO> studentDTOs = _studentService.GetStudents();
                var students = _mapper.Map<IEnumerable<StudentLiteDTO>, List<StudentViewModel>>(studentDTOs);
                return View(students);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }


        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<CreateStudentDTO>(studentViewModel);
                _studentService.CreateStudent(studentDTO);
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }

            return RedirectToAction("Index");

        }

        public IActionResult DeleteStudent(int? id)
        {
            try
            {
                _studentService.DeleteStudent(id);
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _studentService.Dispose();
            base.Dispose(disposing);
        }



        /*        public IActionResult Privacy()
                {
                    return View();
                }*/



        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
