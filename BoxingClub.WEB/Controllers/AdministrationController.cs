﻿using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Implementation;
using BoxingClub.Web.WebManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName)]
    public class AdministrationController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAdministrationWebManager _administrationWebManager;

        public AdministrationController(IRoleService roleService,
                                        IUserService userService,
                                        IMapper mapper,
                                        IAdministrationWebManager administrationWebManager)
        {   
            _roleService = roleService;
            _userService = userService;
            _mapper = mapper;
            _administrationWebManager = administrationWebManager;
        }

        [HttpGet]
        [Route("Administration/GetUsers")]
        public async Task<IActionResult> GetUsers(SearchModelDTO searchModel)
        {
            var pageViewModel = await _administrationWebManager.GetUsersAsync(searchModel);
            var sizes = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = searchModel.PageSize ?? 3;

            return View(pageViewModel);
        }

        [HttpDelete("{id}")]
        [Route("Administration/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return RedirectToAction("GetUsers", "Administration");
        }

        [HttpGet]
        [Route("Administration/DetailsUser/{id}")]
        public async Task<IActionResult> DetailsUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);
            var mappedUser = _mapper.Map<UserViewModel>(user);
            return View(mappedUser);
        }

        [HttpGet]
        [Route("Administration/EditUser/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);
            var mappedUser = _mapper.Map<UserViewModel>(user);
            ViewBag.Roles = await GetRoles();
            return View(mappedUser);
        }

        [HttpPost]
        [Route("Administration/EditUser/{id}")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<UserDTO>(model);
                var result = await _userService.UpdateUserAsync(mappedModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers", "Administration");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.Roles = await GetRoles();
            return View(model);
        }
        private async Task<SelectList> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            var mappedRoles = _mapper.Map<List<RoleViewModel>>(roles);
            var selectList = new SelectList(mappedRoles, "Id", "Name");
            return selectList;
        }
    }
}

