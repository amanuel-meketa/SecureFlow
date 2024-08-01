using Logto.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecurityService.Models;
using System.Diagnostics;

namespace SecurityService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public IActionResult SignIn()
        {
            // This will redirect the user to the Logto sign-in page.
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }

        // Use the `new` keyword to avoid conflict with the `ControllerBase.SignOut` method
        new public IActionResult SignOut()
        {
            // This will clear the authentication cookie and redirect the user to the Logto sign-out page
            // to clear the Logto session as well.
            return SignOut(new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}
