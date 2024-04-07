using Dtos.AlphaBank.Security;
using Interfaces.BankAccounts.Services;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Services;

namespace WebClient.Controllers {
    public class MonitoringLoanStatusController(IUserAuthenticatorService userAuthenticatorService,
                                    ICustomerService customerService) : Controller {

        private readonly IUserAuthenticatorService _userAuthenticatorService = userAuthenticatorService;
        private readonly ICustomerService _customerService = customerService;
        public IActionResult Login() =>
            User.Identity!.IsAuthenticated ? Redirect("~/Home/Index") : View();


        public async Task<IActionResult> CustomerList() {


            View(await _customerService.GetAll());

            string script = "<script>AlertSuccess('Carga exitosa','La información se cargó correctamente');</script>";

            // Pasar el script a la vista utilizando ViewBag
            ViewBag.AlertSuccess = script;
            return View();
        }

        public IActionResult MonitoringLoanStatus() => View();     
    }
}