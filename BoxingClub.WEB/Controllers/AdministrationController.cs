using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdministrationController(IRoleService roleService,
                                        IUserService userService,
                                        IMapper mapper)
        {
            _roleService = roleService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var mappedRoles = _mapper.Map<List<UserViewModel>>(users);
            return View(mappedRoles);
        }

        public async Task<IActionResult> DeleteUser(string? id)
        {
            
        }



        /*[HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<RoleDTO>(model);
                var result = await _roleService.CreateRoleAsync(role);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _mapper.Map<List<RoleViewModel>>(await _roleService.GetRolesAsync());
            return View(roles);
        }

        [HttpGet]
        [Route("Administration/EditRole/{id}")]
        public async Task<IActionResult> EditRole(string? id)
        {
            var role = await _roleService.FindRoleByIdAsync(id);
            return View(_mapper.Map<RoleViewModel>(role));
        }

        [HttpPost]
        [Route("Administration/EditRole/{id}")]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<IdentityRole>(model);
                var result = await _roleService.EditRoleAsync(_mapper.Map<RoleDTO>(role));
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles", "administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [Route("Administration/DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string? id)
        {
            await _roleService.DeleteRoleAsync(id);
            return RedirectToAction("GetRoles", "administration");
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string? roleId)
        {
            var role = await _roleService.FindRoleByIdAsync(roleId);
            ViewBag.roleName = role.Name;
            ViewBag.roleId = role.Id;
            var model = new List<UserRoleViewModel>();

            var users = await _userService.GetUsersAsync();
            foreach (var user in users)
            {
                var userRoleViewModel = _mapper.Map<UserRoleViewModel>(user);
                userRoleViewModel.IsSelected = await _userService.IsInRoleAsync(user, role.Name);
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> userView, string? roleId)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleService.FindRoleByIdAsync(roleId);
                for (var i = 0; i < userView.Count; i++)
                {
                    var user = await _userService.FindUserByIdAsync(userView[i].UserId);
                    IdentityResult result;

                    if (userView[i].IsSelected && !(await _userService.IsInRoleAsync(user, role.Name)))
                    {
                        result = _mapper.Map<IdentityResult>(await _userService.AddToRoleAsync(user, role.Name));
                    }
                    else if (!userView[i].IsSelected && (await _userService.IsInRoleAsync(user, role.Name)))
                    {
                        result = _mapper.Map<IdentityResult>(await _userService.RemoveFromRoleAsync(user, role.Name));
                    }
                    else
                    {
                        continue;
                    }

                    if (result.Succeeded)
                    {
                        if (i < (userView.Count - 1))
                        {
                            continue;
                        }
                        else
                        {
                            return RedirectToAction("EditRole", new { Id = roleId });
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }*/
    }
}

