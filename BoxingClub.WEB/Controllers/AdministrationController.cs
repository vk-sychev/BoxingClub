using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Helpers;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Implementation;
using BoxingClub.Web.WebManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities.Models;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.HttpClients.Interfaces;
using Newtonsoft.Json;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName)]
    [Route("[controller]")]
    public class AdministrationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserClient _userClient;

        public AdministrationController(IMapper mapper, 
                                        IUserClient userClient)
        {   
            _mapper = mapper;
            _userClient = userClient;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers(SearchModelDTO searchModel)
        {
            var token = Request.Cookies["token"];
            var response = await _userClient.GetUsers(searchModel, token);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("AccessDenied");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var content = await response.Content.ReadAsStringAsync();
            var pageViewModel = JsonConvert.DeserializeObject<PageViewModel<UserViewModel>>(content);

            var sizes = PageSizeHelper.GetPageSizeList(7);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = pageViewModel.PageSize;

            return View(pageViewModel);
        }

        [HttpDelete("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var token = Request.Cookies["token"];
            var response = await _userClient.DeleteUser(id, token);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("AccessDenied");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            return RedirectToAction("GetUsers", "Administration");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DetailsUser(string id)
        {
            var token = Request.Cookies["token"];
            var response = await _userClient.GetUser(id, token);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("AccessDenied");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserViewModel>(content);
            return View(user);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EditUser(string id)
        {
            var token = Request.Cookies["token"];
            var response = await _userClient.GetUser(id, token);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("AccessDenied");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserViewModel>(content);

            response = await GetRoles();

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("AccessDenied");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            content = await response.Content.ReadAsStringAsync();
            
            var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(content);
            ViewBag.Roles = GetRolesSelectList(roles);
            return View(user);
        }

        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var response = await _userClient.EditUser(token, model);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return View("AccessDenied");
                    }

                    throw new InvalidOperationException("Error occurred while processing your request");
                }

                return RedirectToAction("GetUsers", "Administration");
            }

            var resp = await GetRoles();

            if (!resp.IsSuccessStatusCode)
            {
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("AccessDenied");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            var content = await resp.Content.ReadAsStringAsync();

            var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(content);
            ViewBag.Roles = GetRolesSelectList(roles);
            return View(model);
        }

        private async Task<HttpResponseMessage> GetRoles()
        {
            var token = Request.Cookies["token"];
            return await _userClient.GetRoles(token);
        }

        private SelectList GetRolesSelectList(List<RoleViewModel> roles)
        {
            var mappedRoles = _mapper.Map<List<RoleViewModel>>(roles);
            return new SelectList(mappedRoles, "Id", "Name");
        }
    }
}

