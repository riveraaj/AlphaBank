using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Services;

namespace WebClient.Controllers {
    public class SecurityController(IUserAuthenticatorService userAuthenticatorService,
                                    IEmployeeService employeeService) : Controller {

        private readonly IUserAuthenticatorService _userAuthenticatorService = userAuthenticatorService;
        private readonly IEmployeeService _employeeService = employeeService;

        public IActionResult Login() =>
            User.Identity!.IsAuthenticated ? Redirect("~/Home/Index") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id", "Password")] UserLoginDTO oUserLoginDto) {

            if (!ModelState.IsValid) return View();

            var (result, userAuthentication) = await _userAuthenticatorService.UserAuthenticator(oUserLoginDto);

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

        [Authorize(Roles = "1")]
        public async Task<IActionResult> EmployeeList() =>
            View(await _employeeService.GetAll());
    }
}