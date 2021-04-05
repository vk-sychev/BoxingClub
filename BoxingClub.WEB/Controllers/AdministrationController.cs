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
    [AllowAnonymous]
    //[Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AdministrationController(IAccountService accountService,
                                 IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
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
                var result = await _accountService.CreateRole(role);
                
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

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _mapper.Map<List<RoleViewModel>>(_accountService.GetRoles());
            return View(roles);
        }

        [HttpGet]
        [Route("EditRole/{id}")]
        public async Task<IActionResult> EditRole(string? id)
        {
            var role = await _accountService.FindRoleById(id);
            return View(_mapper.Map<RoleViewModel>(role));
        }

        [HttpPost]
        [Route("EditRole/{id}")]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<IdentityRole>(model);
                var result = await _accountService.EditRole(_mapper.Map<RoleDTO>(role));
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

        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string? id)
        {
            await _accountService.Delete(id);
            return RedirectToAction("GetRoles", "administration");
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string? roleId)
        {
            var role = await _accountService.FindRoleById(roleId);
            ViewBag.roleName = role.Name;
            ViewBag.roleId = role.Id;
            var model = new List<UserRoleViewModel>();

            var users = _accountService.GetUsers();
            //var userRole = _mapper.Map<UserRoleViewModel>(users.First());
            foreach (var user in users)
            {
                var userRoleViewModel = _mapper.Map<UserRoleViewModel>(user);
                userRoleViewModel.IsSelected = await _accountService.IsInRole(user, role.Name);
/*                if (await _accountService.IsInRole(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }*/
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> userView, string? roleId)
        {
            if (ModelState.IsValid)
            {
                var role = await _accountService.FindRoleById(roleId);
                for (var i = 0; i < userView.Count; i++)
                {
                    var user = await _accountService.FindUserById(userView[i].UserId);
                    IdentityResult result;

                    if (userView[i].IsSelected && !(await _accountService.IsInRole(user, role.Name)))
                    {
                        result = _mapper.Map<IdentityResult>(await _accountService.AddToRole(user, role.Name));
                    }
                    else if (!userView[i].IsSelected && (await _accountService.IsInRole(user, role.Name)))
                    {
                        result = _mapper.Map<IdentityResult>(await _accountService.RemoveFromRole(user, role.Name));
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
        }
    }
}

