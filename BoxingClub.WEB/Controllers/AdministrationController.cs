using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.Helpers;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities.Models;
using BoxingClub.Infrastructure.CustomAttributes;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.CustomAttributes;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Models;
using Newtonsoft.Json;
using AuthorizeRoles = BoxingClub.Web.CustomAttributes.AuthorizeRolesAttribute;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName)]
    [Route("[controller]")]
    public class AdministrationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserClientAdapter _userClientAdapter;

        public AdministrationController(IMapper mapper,
                                        IUserClientAdapter userClientAdapter)
        {
            _mapper = mapper;
            _userClientAdapter = userClientAdapter;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers(SearchModelDTO searchModel)
        {
            var token = Request.Cookies["token"];
            var mappedModel = _mapper.Map<SearchModel>(searchModel);
            var response = await _userClientAdapter.GetUsers(mappedModel, token);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var pageViewModel = response.Items;
            var mappedPageModel = _mapper.Map<PageViewModel<UserViewModel>>(pageViewModel);

            var sizes = PageSizeHelper.GetPageSizeList(7);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = mappedPageModel.PageSize;

            return View(mappedPageModel);
        }

        [HttpDelete("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var token = Request.Cookies["token"];
            var response = await _userClientAdapter.DeleteUser(id, token);

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("GetUsers", "Administration");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DetailsUser(string id)
        {
            var token = Request.Cookies["token"];
            var response = await _userClientAdapter.GetUser(id, token);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedUser = _mapper.Map<UserViewModel>(response.Item);
            return View(mappedUser);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EditUser(string id)
        {
            var token = Request.Cookies["token"];
            var response = await _userClientAdapter.GetUser(id, token);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedUser = _mapper.Map<UserViewModel>(response.Item);

            var resp = await GetRoles();

            redirect = GetRedirectAction(resp.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedRoles = _mapper.Map<List<RoleViewModel>>(resp.Items);
            ViewBag.Roles = GetRolesSelectList(mappedRoles);
            return View(mappedUser);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedUser = _mapper.Map<UserModel>(model);
                var response = await _userClientAdapter.EditUser(token, mappedUser);

                var redirect = GetRedirectAction(response.StatusCode);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("GetUsers", "Administration");
            }

            var resp = await GetRoles();

            var redir = GetRedirectAction(resp.StatusCode);
            if (redir != null)
            {
                return redir;
            }

            var mappedRoles = _mapper.Map<List<RoleViewModel>>(resp.Items);

            ViewBag.Roles = GetRolesSelectList(mappedRoles);
            return View(model);
        }

        private async Task<ItemsResponseModel<RoleModel>> GetRoles()
        {
            var token = Request.Cookies["token"];
            return await _userClientAdapter.GetRoles(token);
        }

        private SelectList GetRolesSelectList(List<RoleViewModel> roles)
        {
            return new SelectList(roles, "Id", "Name");
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
