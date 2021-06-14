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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityServer.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IAdministrationWebManager _administrationWebManager;

        public AdministrationController(IUserService userService,
                                        IRoleService roleService,
                                        IMapper mapper,
                                        IAdministrationWebManager administrationWebManager)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _administrationWebManager = administrationWebManager;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers(SearchModelDTO searchModel)
        {
            var pageViewModel = await _administrationWebManager.GetUsersAsync(searchModel);
            return Ok(pageViewModel);
        }

        [HttpDelete("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);
            var mappedUser = _mapper.Map<UserViewModel>(user);
            return Ok(mappedUser);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            var result = new AccountResultDTO();
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<UserDTO>(model);
                result = await _userService.UpdateUserAsync(mappedModel);
                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<RoleViewModel>> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }
    }
}
