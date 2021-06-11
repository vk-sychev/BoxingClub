using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Helpers;
using IdentityServer.BLL.Entities;
using IdentityServer.BLL.Interfaces;
using IdentityServer.Models;
using IdentityServer.WebManagers.Implementation;
using IdentityServer.WebManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityServer.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAdministrationWebManager _administrationWebManager;

        public AdministrationController(IUserService userService,
                                        IMapper mapper,
                                        IAdministrationWebManager administrationWebManager)
        {
            _userService = userService;
            _mapper = mapper;
            _administrationWebManager = administrationWebManager;
        }

        [HttpGet]
        [Route("Administration/GetUsers")]
        public async Task<IActionResult> GetUsers(SearchModelDTO searchModel)
        {
            var pageViewModel = await _administrationWebManager.GetUsersAsync(searchModel);
            var sizes = PageSizeHelper.GetPageSizeList(7);
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = searchModel.PageSize;

            return Ok(pageViewModel);
        }

        [HttpDelete("{id}")]
        [Route("Administration/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return Ok();
            //return RedirectToAction("GetUsers", "Administration");
        }

        [HttpGet]
        [Route("Administration/DetailsUser/{id}")]
        public async Task<IActionResult> DetailsUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);
            var mappedUser = _mapper.Map<UserViewModel>(user);
            return Ok(mappedUser);
        }

        [HttpGet]
        [Route("Administration/EditUser/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);
            var mappedUser = _mapper.Map<UserViewModel>(user);
            //ViewBag.Roles = await GetRoles();
            return Ok(mappedUser);
        }

        [HttpPost]
        [Route("Administration/EditUser/{id}")]
        public async Task<HttpStatusCode> EditUser(UserViewModel model)
        {
            var result = new AccountResultDTO();
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<UserDTO>(model);
                result = await _userService.UpdateUserAsync(mappedModel);
                if (result.Succeeded)
                {
                    return HttpStatusCode.OK;
                }

                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.BadRequest;
        }
    }
}
