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

        private IStudentService _studentService;

        public HomeController(ILogger<HomeController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<IndexStudentDTO> studentDTOs = _studentService.GetStudents();
                var mapper = new MapperConfiguration(s => s.CreateMap<IndexStudentDTO, StudentViewModel>()).CreateMapper();
                var students = mapper.Map<IEnumerable<IndexStudentDTO>, List<StudentViewModel>>(studentDTOs);
                return View(students);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
            
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
