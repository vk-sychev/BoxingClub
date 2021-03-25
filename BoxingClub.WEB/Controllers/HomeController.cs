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

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<StudentLiteDTO> studentDTOs = await _studentService.GetStudents();
                var students = _mapper.Map<IEnumerable<StudentLiteDTO>, List<StudentViewModel>>(studentDTOs);
                return View(students);
            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.CreateStudent(studentDTO);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DeleteStudent(int? id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
                //ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            return RedirectToAction("Error"); ;
        }

        public async Task<IActionResult> UpdateStudent(int? id)
        {
            //CreateStudentDTO student = new CreateStudentDTO();
            CreateStudentViewModel student = new CreateStudentViewModel();
            try
            {
                //student =_studentService.GetStudent(id.Value);
                var studentDTO = await _studentService.GetStudent(id.Value);
                student = _mapper.Map<CreateStudentViewModel>(studentDTO);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(CreateStudentViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.UpdateStudent(studentDTO);
            }
            catch (ArgumentNullException ex)
            {
                return Content(ex.Message);
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
