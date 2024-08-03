using static Logto.Authentication.extensions.HttpContextExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecurityService.Models;
using System.Diagnostics;
using Logto.Authentication;

namespace SecurityService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var logtoOptions = HttpContext.GetLogtoOptions();
            ViewData["Resource"] = logtoOptions.Resource;
            ViewData["AccessToken"] = await HttpContext.GetTokenAsync(LogtoParameters.Tokens.AccessToken);
   
            var claims = User.Claims;

            // Get the user ID
            var userId = claims.FirstOrDefault(c => c.Type == LogtoParameters.Claims.Subject)?.Value;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
