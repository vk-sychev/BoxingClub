using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoxingClub.Infrastructure.Constants;
using System.Net;
using BoxingClub.Infrastructure.Helpers;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Models;
using AuthorizeRoles = BoxingClub.Web.CustomAttributes.AuthorizeRolesAttribute;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;
using BoxingClub.BLL.DomainEntities;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentClientAdapter _studentClientAdapter;
        private readonly IMapper _mapper;

        public StudentController(IMapper mapper,
                                 IStudentClientAdapter studentClientAdapter)
        {
            _mapper = mapper;
            _studentClientAdapter = studentClientAdapter;
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents(SearchModelDTO searchModel)
        {
            var token = Request.Cookies["token"];

            var mappedModel = _mapper.Map<SearchModel>(searchModel);
            var response = await _studentClientAdapter.GetStudents(token, mappedModel);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
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

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
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

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("GetStudents", "Student");
            }

            var boxingGroupResponse = await GetGroups();

            var redir = GetRedirectAction(boxingGroupResponse.StatusCode);
            if (redir != null)
            {
                return redir;
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

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("GetStudents", "Student");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditStudent(int id, bool fromHomeController, int returnId)
        {
            var boxingGroupResponse = await GetGroups();

            var redirect = GetRedirectAction(boxingGroupResponse.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedGroups = _mapper.Map<List<BoxingGroupLiteViewModel>>(boxingGroupResponse.Items);
            ViewBag.Groups = new SelectList(mappedGroups, "Id", "Name");

            ViewBag.fromHomeController = fromHomeController;
            ViewBag.returnId = returnId;

            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.GetStudent(token, id);

            var redir = GetRedirectAction(response.StatusCode);
            if (redir != null)
            {
                return redir;
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

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                if (fromHomeController)
                {
                    return RedirectToAction("DetailsBoxingGroup", "Home", new { id = returnId });
                }
                return RedirectToAction("GetStudents", "Student");
            }


            var boxingGroupResponse = await GetGroups();

            var redir = GetRedirectAction(boxingGroupResponse.StatusCode);
            if (redir != null)
            {
                return redir;
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

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
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

        private IActionResult GetRedirectAction(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            return null;
        }
    }
}
