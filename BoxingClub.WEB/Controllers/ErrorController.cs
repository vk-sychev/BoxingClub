using BoxingClub.Infrastructure.HttpSwitcher;
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
            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                Message = exceptionDetails.Error.Message,
                StatusCode = HttpCodeHelper.GetSwitchHttpCode(exceptionDetails.Error.GetType())
            };
            _logger.LogError(exceptionDetails.Error.Message);
            return View(errorViewModel);
        }
    }
}
