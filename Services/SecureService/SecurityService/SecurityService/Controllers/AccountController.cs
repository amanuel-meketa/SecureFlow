using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SecurityService.Controllers
{
    public class AccountController : Controller
        {
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
