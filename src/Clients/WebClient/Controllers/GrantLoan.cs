using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Services;
using WebClient.Models;
using Data.AlphaBank;
using Service.BankAccounts;

namespace WebClient.Controllers

{
    public class GrantLoanController(IUserAuthenticatorService userAuthenticatorService,
                                    ICustomerService customerService) : Controller
    {

        private readonly IUserAuthenticatorService _userAuthenticatorService = userAuthenticatorService;
        private readonly ICustomerService _customerService = customerService;
        public IActionResult Login() =>
            User.Identity!.IsAuthenticated ? Redirect("~/Home/Index") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id", "Password")] UserLoginDTO oUserLoginDTO)
        {

            if (!ModelState.IsValid) return View();

            var (result, userAuthentication) = await _userAuthenticatorService.UserAuthenticator(oUserLoginDTO);

            if (!result)
            {
                ViewData["Error"] = "*Hubo un error en el inicio de sesión, intentelo más tarde.";
                return View();
            }

            await CookiesService.CreateAuthenticationCookies(HttpContext, userAuthentication!);

            return Redirect("~/Home/Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await CookiesService.RemoveAuthenticationCookie(HttpContext);
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> CustomerList()
        {


            View(await _customerService.GetAll());

            string script = "<script>AlertSuccess('Carga exitosa','La información se cargó correctamente');</script>";

            // Pasar el script a la vista utilizando ViewBag
            ViewBag.AlertSuccess = script;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EvaluationLoanApplications()
        {

            return View();
        }

    }
}
