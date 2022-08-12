using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.BLL.Entities;
using IdentityServer.BLL.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountController(IMapper mapper,
                                 IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var result = new AccountResultDTO();
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserDTO>(model);
                result = await _userService.SignUpAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest(result.Errors);
        }
    }
}
