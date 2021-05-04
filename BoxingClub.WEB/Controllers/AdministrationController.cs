using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Models;
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

        public AdministrationController(IRoleService roleService,
                                        IUserService userService,
                                        IMapper mapper)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService), "roleService is null");
            _userService = userService ?? throw new ArgumentNullException(nameof(userService), "userService is null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int? pageIndex, int? pageSize)
        {
            var pageModel = await _userService.GetUsersPaginatedAsync(pageIndex ?? 1, pageSize ?? 3);
            if (!pageModel.Items.Any())
            {
                pageModel = await _userService.GetUsersPaginatedAsync(1, pageSize ?? 3);
                pageIndex = 1;
            }

            var users = _mapper.Map<List<UserViewModel>>(pageModel.Items);
            var pageViewModel = new PageViewModel<UserViewModel>(pageModel.Count, pageIndex ?? 1, pageSize ?? 3, users);

            var sizes = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            ViewBag.Sizes = sizes;
            ViewBag.pageSize = pageSize ?? 3;

            return View(pageViewModel);
        }

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

        private async Task<SelectList> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            var mappedRoles = _mapper.Map<List<RoleViewModel>>(roles);
            var selectList = new SelectList(mappedRoles, "Id", "Name");
            return selectList;
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

    }
}

