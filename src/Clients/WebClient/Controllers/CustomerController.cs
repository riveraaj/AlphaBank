using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers {

    [Authorize]
    public class CustomerController(ICustomerService customerService) : Controller {

        private readonly ICustomerService _customerService = customerService;
   
        public async Task<IActionResult> CustomerList() =>
            View(await _customerService.GetAll());
        
        public ActionResult ShowCustomer()
            => PartialView("CreateCustomer");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel  oCustomerViewModel) {
            string script;
            var customer = oCustomerViewModel.CreateCustomer;
            var result = await _customerService.Create(customer);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido abrir la cuenta, inténtelo más tarde.');</script>";

                ViewData["AlertError"] = script;
                return View("AccountOppening");
            }
            script = "<script>AlertSuccess('Cuenta creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return View();
        }
    }
}