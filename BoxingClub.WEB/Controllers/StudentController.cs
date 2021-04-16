using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoxingClub.WEB.Controllers
{
    [Authorize(Roles = "Manager, Admin, Coach")]
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

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var studentDTOs = await _studentService.GetStudentsAsync();
            var students = _mapper.Map<List<StudentLiteViewModel>>(studentDTOs);
            return View(students);
        }

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> CreateStudent()
        {
            ViewBag.Groups = await GetGroups();
            return View();
        }

        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("GetAllStudents", "Student");
        }

        [Authorize(Roles = "Manager, Admin")]
        private async Task<SelectList> GetGroups()
        {
            var groups = await _boxingGroupService.GetBoxingGroupsAsync();
            var groupViewModels = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(groupViewModels, "Id", "Name");
            return selectList;
        }

        [Authorize(Roles = "Manager, Admin")]
        [Route("Student/UpdateStudent/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateStudent(int? id, bool fromHomeController, int returnId)
        {

            ViewBag.Groups = await GetGroups();
            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            var studentDTO = await _studentService.GetStudentAsync(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);

            return View(student);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        [Route("Student/UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(StudentFullViewModel studentViewModel, bool fromHomeController, int returnId)
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

        [Authorize(Roles = "Manager, Admin, Coach")]
        [HttpGet]
        [Route("Student/DetailsStudent/{id}")]
        public async Task<IActionResult> DetailsStudent(int? id, bool fromHomeController, int returnId)
        {
            var studentDTO = await _studentService.GetStudentAsync(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);
            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            return View(student);
        }
    }
}
