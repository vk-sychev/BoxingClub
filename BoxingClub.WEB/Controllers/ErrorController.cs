using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Infrastructure.Helpers;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoxingClub.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = HttpCodeHelper.GetSwitchHttpCode(exceptionDetails.Error.GetType());
            _logger.LogError(exceptionDetails.Error.Message);
            ViewBag.Message = error.Message;
            return View();
        }
    }
}
