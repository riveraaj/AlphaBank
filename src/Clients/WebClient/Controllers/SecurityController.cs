using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Services;

namespace WebClient.Controllers {
    public class SecurityController(IUserAuthenticatorService userAuthenticatorService) : Controller {

        private readonly IUserAuthenticatorService _userAuthenticatorService = userAuthenticatorService;

        public IActionResult Login() =>
            User.Identity!.IsAuthenticated ? Redirect("~/Home/Index") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id", "Password")] UserLoginDTO oUserLoginDTO) {

            if (!ModelState.IsValid) return View();

            var (result, userAuthentication) = await _userAuthenticatorService.UserAuthenticator(oUserLoginDTO);

            if (!result) {
                ViewData["Error"] = "*Hubo un error en el inicio de sesión, intentelo más tarde.";
                return View();
            }

            await CookiesService.CreateAuthenticationCookies(HttpContext, userAuthentication!);

            return Redirect("~/Home/Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await CookiesService.RemoveAuthenticationCookie(HttpContext);
            return RedirectToAction("Login");
        }
    }
}