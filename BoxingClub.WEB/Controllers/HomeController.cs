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
            List<StudentLiteViewModel> students = new List<StudentLiteViewModel>();
            try
            {
                IEnumerable<StudentLiteDTO> studentDTOs = await _studentService.GetStudents();
                students = _mapper.Map<IEnumerable<StudentLiteDTO>, List<StudentLiteViewModel>>(studentDTOs);
            }

            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                //_logger.LogError()
                return NotFound(ModelState); 
            }

            catch (Exception ex)
            {
                return BadRequest();
            }

            return View(students);
        }


        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.CreateStudent(studentDTO);
            }

            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return NotFound(ModelState);
            }

            catch (Exception ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DeleteStudent(int? id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
            }

            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return BadRequest(ModelState);
            }

            catch (Exception ex)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateStudent(int? id)
        {
            StudentFullViewModel student = new StudentFullViewModel();
            try
            {
                var studentDTO = await _studentService.GetStudent(id.Value);
                student = _mapper.Map<StudentFullViewModel>(studentDTO);
            }

            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return NotFound(ModelState);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(StudentFullViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.UpdateStudent(studentDTO);
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
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
