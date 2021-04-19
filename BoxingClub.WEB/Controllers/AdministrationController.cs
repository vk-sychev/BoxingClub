using AutoMapper;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Controllers
{
    [Authorize(Roles = Constants.AdminRoleName)]
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
            var mappedUsers = _mapper.Map<List<UserViewModel>>(users);
            return View(mappedUsers);
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
    }
}

