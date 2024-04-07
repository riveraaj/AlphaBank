using Interfaces.BankAccounts.Services;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class InterestRatesController(IUserAuthenticatorService userAuthenticatorService,
                                    ICustomerService customerService) : Controller {

        private readonly IUserAuthenticatorService _userAuthenticatorService = userAuthenticatorService;
        private readonly ICustomerService _customerService = customerService;

        public async Task<IActionResult> InterestRatesList()
        {


            View(await _customerService.GetAll());

            string script = "<script>AlertSuccess('Carga exitosa','La información se cargó correctamente');</script>";

            // Pasar el script a la vista utilizando ViewBag
            ViewBag.AlertSuccess = script;
            return View();
        }


        [HttpGet]
        public IActionResult InterestRates() => View();
        

    }
}
