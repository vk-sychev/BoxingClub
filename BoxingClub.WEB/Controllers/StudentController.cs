using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.DomainEntities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoxingClub.Infrastructure.Constants;
using System.Linq;
using BoxingClub.Infrastructure.Exceptions;
using System;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.WebManagers.Interfaces;
using BoxingClub.Web.Helpers;
using AuthorizeRoles = BoxingClub.Web.CustomAttributes.AuthorizeRolesAttribute;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IMedicalCertificateService _medicalCertificateService;
        private readonly IStudentWebManager _studentWebManager;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService,
                                 IMapper mapper,
                                 IBoxingGroupService boxingGroupService,
                                 IMedicalCertificateService medicalCertificateService,
                                 IStudentWebManager studentWebManager)
        {
            _studentService = studentService;
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _medicalCertificateService = medicalCertificateService;
            _studentWebManager = studentWebManager;
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Student/GetAllStudents")]
        public async Task<IActionResult> GetAllStudents(SearchModelDTO searchModel)
        {
            var pageViewModel = await _studentWebManager.GetStudentsAsync(searchModel);

            var sizes = PageSizeHelper.GetPageSizeList(5);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = searchModel.PageSize;
            ViewBag.experienceFilter = searchModel.ExperienceFilter ?? 0;
            ViewBag.medExaminationFilter = searchModel.MedExaminationFilter ?? 0;

            return View(pageViewModel);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet]
        [Route("Student/CreateStudent")]
        public async Task<IActionResult> CreateStudent()
        {
            ViewBag.Groups = await GetGroups();
            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Student/CreateStudent")]
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

        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("Student/DeleteStudent/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("GetAllStudents", "Student");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("Student/EditStudent/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditStudent(int id, bool fromHomeController, int returnId)
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
                    return RedirectToAction("DetailsBoxingGroup", "Home", new { id = returnId });
                }
                return RedirectToAction("GetAllStudents", "Student");
            }
            ViewBag.Groups = await GetGroups();
            ViewBag.fromHomeController = fromHomeController;

            return View(studentViewModel);
        }

        [HttpGet]
        [Route("Student/DetailsStudent/{id}")]
        public async Task<IActionResult> DetailsStudent(int id, bool fromHomeController, int returnId)
        {
            var studentDTO = await _studentService.GetStudentByIdAsync(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);
            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            return View(student);
        }

        private async Task<SelectList> GetGroups()
        {
            var token = Request.Cookies["token"];
            var groups = await _boxingGroupService.GetBoxingGroupsAsync(token);
            var groupViewModels = _mapper.Map<List<BoxingGroupLiteViewModel>>(groups);
            var selectList = new SelectList(groupViewModels, "Id", "Name");
            return selectList;
        }
    }
}
