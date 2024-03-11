using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class HomeController
        (ILogger<HomeController> logger, IUserAuthenticatorService oUserService) : Controller {

        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUserAuthenticatorService _userService = oUserService;

        public IActionResult Index()
             => View();

        [Authorize]
        public IActionResult Privacy()
        => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id", "Password")] UserLoginDto oUserLoginDto) {

            if (!ModelState.IsValid) return View("Index");

            var (result, userAuthentication) = await _userService.UserAuthenticator(oUserLoginDto);

            if (!result) {
                ViewData["Error"] = "*Hubo un error en el inicio de sesión, intentelo más tarde.";
                return View("Index");
            }

            await CookiesService.CreateAuthenticationCookies(HttpContext, userAuthentication!);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()  {
            await CookiesService.RemoveAuthenticationCookie(HttpContext);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}