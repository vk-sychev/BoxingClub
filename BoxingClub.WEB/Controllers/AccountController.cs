using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BoxingClub.Web.HttpClients.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using IAuthenticationService = BoxingClub.BLL.Interfaces.IAuthenticationService;


namespace BoxingClub.Web.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _signInService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserClient _userClient;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthenticationService signInService,
                                 IUserService userService,
                                 IMapper mapper,
                                 IUserClient userClient,
                                 ILogger<AccountController> logger)
        {
            _signInService = signInService;
            _userService = userService;
            _mapper = mapper;
            _userClient = userClient;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
                try
                {
                    response = await _userClient.SignUpAsync(model);

                    if (response.IsSuccessStatusCode)
                    {
                        var token = await _userClient.GetTokenAsync(model.UserName, model.Password);
                        if (!string.IsNullOrEmpty(token))
                        {
                            var decodedToken = AppendTokenInCookie(token);
                            await SignInCookie(decodedToken, true);
                            return RedirectToAction("index", "home");
                        }

                        ModelState.AddModelError("", "Invalid Login Attempt");
                    }
                }
                catch
                {
                    return View(model);
                }

/*                var user = _mapper.Map<UserDTO>(model);
                var result = await _userService.SignUpAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }*/

/*                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }*/
            }
            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> SignOut()
        {
            HttpContext.Response.Cookies.Delete("token");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SignIn(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = string.Empty;
                try
                {
                    token = await _userClient.GetTokenAsync(model.UserName, model.Password);
                }

                catch
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                    return View(model);
                }

                if (!string.IsNullOrEmpty(token))
                {
/*                    var decodedToken = (JwtSecurityToken) new JwtSecurityTokenHandler().ReadToken(token);
                    HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
                    {
                        Expires = decodedToken.ValidTo
                    });*/
                    var decodedToken = AppendTokenInCookie(token);
                    await SignInCookie(decodedToken, model.RememberMe);
                    /*ClaimsIdentity identity = new ClaimsIdentity(decodedToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme, 
                        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = true });*/

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }

        private async Task SignInCookie(JwtSecurityToken token, bool isPersistent)
        {
            ClaimsIdentity identity = new ClaimsIdentity(token.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                principal, 
                new AuthenticationProperties()
                {
                    IsPersistent = isPersistent
                });
        }

        private JwtSecurityToken AppendTokenInCookie(string token)
        {
            var decodedToken = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(token);
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
            {
                Expires = decodedToken.ValidTo
            });
            return decodedToken;
        }
    }
}
