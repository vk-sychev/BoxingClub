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
using System.Net;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.WebManagers.Interfaces;
using BoxingClub.Web.Helpers;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Models;
using AuthorizeRoles = BoxingClub.Web.CustomAttributes.AuthorizeRolesAttribute;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IMedicalCertificateService _medicalCertificateService;
        private readonly IStudentWebManager _studentWebManager;
        private readonly IStudentClientAdapter _studentClientAdapter;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService,
                                 IMapper mapper,
                                 IBoxingGroupService boxingGroupService,
                                 IMedicalCertificateService medicalCertificateService,
                                 IStudentWebManager studentWebManager,
                                 IStudentClientAdapter studentClientAdapter)
        {
            _studentService = studentService;
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _medicalCertificateService = medicalCertificateService;
            _studentWebManager = studentWebManager;
            _studentClientAdapter = studentClientAdapter;
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents(SearchModelDTO searchModel)
        {
            var token = Request.Cookies["token"];

            var mappedModel = _mapper.Map<SearchModel>(searchModel);
            var response = await _studentClientAdapter.GetStudents(token, mappedModel);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var pageViewModel = response.Items;
            var mappedPageModel = _mapper.Map<PageViewModel<StudentLiteViewModel>>(pageViewModel);

            var sizes = PageSizeHelper.GetPageSizeList(5);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = searchModel.PageSize;
            ViewBag.experienceFilter = searchModel.ExperienceFilter ?? 0;
            ViewBag.medExaminationFilter = searchModel.MedExaminationFilter ?? 0;

            return View(mappedPageModel);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateStudent()
        {
            var response = await GetGroups();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedGroups = _mapper.Map<List<BoxingGroupLiteViewModel>>(response.Items);

            ViewBag.Groups = new SelectList(mappedGroups, "Id", "Name");
            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedStudent = _mapper.Map<StudentFullModel>(studentViewModel);
                var response = await _studentClientAdapter.CreateStudent(token, mappedStudent);

                if (response != HttpStatusCode.OK)
                {
                    if (response == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("SignOut", "Account");
                    }

                    throw new InvalidOperationException("Error occurred while processing your request");
                }

                return RedirectToAction("GetStudents", "Student");
            }

            var boxingGroupResponse = await GetGroups();

            if (boxingGroupResponse.StatusCode != HttpStatusCode.OK)
            {
                if (boxingGroupResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedGroups = _mapper.Map<List<BoxingGroupLiteViewModel>>(boxingGroupResponse.Items);

            ViewBag.Groups = new SelectList(mappedGroups, "Id", "Name");
            return View(studentViewModel);

        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("[action]/{id}")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.DeleteStudent(token, id);

            if (response != HttpStatusCode.OK)
            {
                if (response == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            return RedirectToAction("GetStudents", "Student");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditStudent(int id, bool fromHomeController, int returnId)
        {
            var boxingGroupResponse = await GetGroups();

            if (boxingGroupResponse.StatusCode != HttpStatusCode.OK)
            {
                if (boxingGroupResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedGroups = _mapper.Map<List<BoxingGroupLiteViewModel>>(boxingGroupResponse.Items);
            ViewBag.Groups = new SelectList(mappedGroups, "Id", "Name");

            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;

            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.GetStudent(token, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedStudent = _mapper.Map<StudentFullViewModel>(response.Item);

            return View(mappedStudent);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> EditStudent(StudentFullViewModel studentViewModel, bool fromHomeController, int returnId)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedStudent = _mapper.Map<StudentFullModel>(studentViewModel);
                var response = await _studentClientAdapter.EditStudent(token, mappedStudent);

                if (response != HttpStatusCode.OK)
                {
                    if (response == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("SignOut", "Account");
                    }

                    throw new InvalidOperationException("Error occurred while processing your request");
                }

                if (fromHomeController)
                {
                    return RedirectToAction("DetailsBoxingGroup", "Home", new { id = returnId });
                }
                return RedirectToAction("GetStudents", "Student");
            }


            var boxingGroupResponse = await GetGroups();

            if (boxingGroupResponse.StatusCode != HttpStatusCode.OK)
            {
                if (boxingGroupResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedGroups = _mapper.Map<List<BoxingGroupLiteViewModel>>(boxingGroupResponse.Items);
            ViewBag.Groups = new SelectList(mappedGroups, "Id", "Name");

            ViewBag.fromHomeController = fromHomeController;
            return View(studentViewModel);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsStudent(int id, bool fromHomeController, int returnId)
        {
            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.GetStudent(token, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var mappedStudent = _mapper.Map<StudentFullViewModel>(response.Item);

            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;
            return View(mappedStudent);
        }

        private async Task<ItemsResponseModel<BoxingGroupLiteModel>> GetGroups()
        {
            var token = Request.Cookies["token"];
            return await _studentClientAdapter.GetBoxingGroups(token);
        }
    }
}
