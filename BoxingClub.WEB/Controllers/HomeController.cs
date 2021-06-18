using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.Helpers;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.CustomAttributes;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BoxingClub.Web.Controllers
{
    [Authorize]
    [Route("")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IStudentService _studentService;
        private readonly IHomeWebManager _homeWebManager;
        private readonly IUserClientAdapter _userClientAdapter;
        private readonly IStudentClientAdapter _studentClientAdapter;

        public HomeController(IMapper mapper,
                              IBoxingGroupService boxingGroupService,
                              IStudentService studentService,
                              IHomeWebManager homeWebManager,
                              IUserClientAdapter userClientAdapter,
                              IStudentClientAdapter studentClientAdapter)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _studentService = studentService;
            _homeWebManager = homeWebManager;
            _userClientAdapter = userClientAdapter;
            _studentClientAdapter = studentClientAdapter;
        }

        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public async Task<IActionResult> Index(SearchModelDTO searchModel)
        {
            if (User.IsInRole(Constants.UserRoleName))
            {
                return View("PendingRoleAssignment");
            }

            var token = Request.Cookies["token"];

            var mappedModel = _mapper.Map<SearchModel>(searchModel);
            var response = await _studentClientAdapter.GetBoxingGroups(token, mappedModel);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var pageViewModel = response.Items;
            var mappedPageModel = _mapper.Map<PageViewModel<BoxingGroupLiteViewModel>>(pageViewModel);

            var sizes = PageSizeHelper.GetPageSizeList(5);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = searchModel.PageSize;

            return View(mappedPageModel);
        }


        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditBoxingGroup(int id)
        {
            var boxingGroupResponse = await GetBoxingGroupById(id);

            var redirect = GetRedirectAction(boxingGroupResponse.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var coachResponse = await GetCoaches();
            redirect = GetRedirectAction(coachResponse.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }
            
            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(boxingGroupResponse.Item);
            var mappedCoaches = _mapper.Map<List<UserViewModel>>(coachResponse.Items);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return View(mappedGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditBoxingGroupInline(int id)
        {
            var boxingGroupResponse = await GetBoxingGroupById(id);

            var redirect = GetRedirectAction(boxingGroupResponse.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var coachResponse = await GetCoaches();

            redirect = GetRedirectAction(coachResponse.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedGroup = _mapper.Map<BoxingGroupLiteViewModel>(boxingGroupResponse.Item);
            var mappedCoaches = _mapper.Map<List<UserViewModel>>(coachResponse.Items);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return PartialView("_CreateEditBoxingGroupPartial", mappedGroup);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> EditBoxingGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedModel = _mapper.Map<BoxingGroupLiteModel>(model);
                var boxingGroupResponse = await _studentClientAdapter.EditBoxingGroup(token, mappedModel);

                var redirect = GetRedirectAction(boxingGroupResponse);

                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("Index", "Home");
            }

            var coachResponse = await GetCoaches();
            var redir = GetRedirectAction(coachResponse.StatusCode);

            if (redir != null)
            {
                return redir;
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(coachResponse.Items);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateBoxingGroup()
        {
            var response = await GetCoaches();
            var redirect = GetRedirectAction(response.StatusCode);

            if (redirect != null)
            {
                return redirect;
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Items);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateBoxingGroupInline()
        {
            var response = await GetCoaches();

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(response.Items);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);

            return PartialView("_CreateEditBoxingGroupPartial");
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBoxingGroup(BoxingGroupLiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedModel = _mapper.Map<BoxingGroupLiteModel>(model);
                var boxingGroupResponse = await _studentClientAdapter.CreateBoxingGroup(token, mappedModel);

                var redirect = GetRedirectAction(boxingGroupResponse);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("Index", "Home");
            }

            var coachResponse = await GetCoaches();

            var redir = GetRedirectAction(coachResponse.StatusCode);
            if (redir != null)
            {
                return redir;
            }

            var mappedCoaches = _mapper.Map<List<UserViewModel>>(coachResponse.Items);
            ViewBag.Coaches = GetCoachesSelectList(mappedCoaches);
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsBoxingGroup(int id)
        {
            var token = Request.Cookies["token"];
            var boxingGroupResponse = await _studentClientAdapter.GetBoxingGroupWithStudents(token, id);

            var redirect = GetRedirectAction(boxingGroupResponse.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var model = _mapper.Map<BoxingGroupFullViewModel>(boxingGroupResponse.Item);
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoxingGroup(int id)
        {
            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.DeleteBoxingGroup(token, id);

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("[action]/{studentId}")]
        [HttpDelete("[action]/{studentId}")]
        public async Task<IActionResult> DeleteFromBoxingGroup(int studentId, int returnId)
        {
            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.DeleteStudentFromBoxingGroup(token, studentId);

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("DetailsBoxingGroup", new { id = returnId });
        }

        private async Task<ItemsResponseModel<UserModel>> GetCoaches()
        {
            var token = Request.Cookies["token"];
            return await _userClientAdapter.GetUsersByRole(token, Constants.CoachRoleName);
        }

        private SelectList GetCoachesSelectList(List<UserViewModel> coaches)
        {
            return new SelectList(coaches, "Id", "FullName");
        }

        private async Task<ItemResponseModel<BoxingGroupLiteModel>> GetBoxingGroupById(int id)
        {
            var token = Request.Cookies["token"];
            return await _studentClientAdapter.GetBoxingGroup(token, id);
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
