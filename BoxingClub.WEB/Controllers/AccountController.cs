using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HttpClientAdapters.Interfaces;
using HttpClients.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace BoxingClub.Web.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserClientAdapter _userClientAdapter;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMapper mapper,
                                 IUserClientAdapter userClientAdapter,
                                 ILogger<AccountController> logger)
        {
            _mapper = mapper;
            _userClientAdapter = userClientAdapter;
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
                var mappedModel = _mapper.Map<SignUpModel>(model);

                var response = await _userClientAdapter.SignUpAsync(mappedModel);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var tokenResponse = await _userClientAdapter.GetTokenAsync(model.UserName, model.Password);
                    var token = string.Empty;

                    if (tokenResponse.StatusCode == HttpStatusCode.OK)
                    {
                        token = tokenResponse.AccessToken;
                        var decodedToken = AppendTokenInCookie(token);
                        await SignInCookie(decodedToken, true);
                        return RedirectToAction("index", "home");
                    }

                    ModelState.AddModelError("", "Invalid Login Attempt");
                }

                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

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
                var response = await _userClientAdapter.GetTokenAsync(model.UserName, model.Password);
                var token = string.Empty;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    token = response.AccessToken;
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                    return View(model);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    var decodedToken = AppendTokenInCookie(token);
                    await SignInCookie(decodedToken, model.RememberMe);

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

            HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            HttpContext.Response.Headers.Add("X-Xss-Protection", "1");
            HttpContext.Response.Headers.Add("X-Frame-Options", "DENY");

            return decodedToken;
        }
    }
}
