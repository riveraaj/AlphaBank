using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers {

    [Authorize(Roles = "1, 5")]
    public class BankAccountsController(ICustomerService customerService,
                                        IAccountService accountService,
                                        BankAccountService bankAccountService,
                                        CommonService commonService) : Controller {

        private readonly ICustomerService _customerService = customerService;
        private readonly IAccountService _accountService = accountService;
        private readonly BankAccountService _bankAccountService = bankAccountService;
        private readonly CommonService _commonService = commonService;

        public async Task<IActionResult> AccountClosing(int? id) {

            if (TempData.TryGetValue("AlertError", out object? error))  ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            if (id.HasValue) { //Validates if the parameter has data
                //A customer is obtained from the id
                var customer = await _customerService.GetByIdForAccount((int)id);
                var accountList = await _accountService.GetByIdForBankAccount((int)id);

                if (customer != null)
                    return View(new AccountClosingViewModel { Customer = customer, AccountList = accountList });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseAccount(string accountId, int personId) {

            string script;

            if (!accountId.IsNullOrEmpty()) {

                var result = await _accountService.Remove(accountId);

                if (!result) {
                    script = "<script>AlertError('Hubo un error','La cuenta aún tiene fondos.');</script>";

                    TempData["AlertError"] = script;
                    return RedirectToAction("AccountClosing", new { id = personId });
                }
                script = $"<script>AlertSuccess('Cuenta cerrada exitosamente','La cuenta {accountId} se inactivo.');</script>";
                
                TempData["AlertSuccess"] = script;
                return RedirectToAction("AccountClosing", new { id = personId });
            }
            return RedirectToAction("AccountClosing");
        }

        [HttpGet]
        public async Task<IActionResult> AccountOppening(int? id){

            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var typeAccountList = await _bankAccountService.GetTypeAccountSelectListItems();
            var typeCurrencyList = await _commonService.GetTypeCurrencySelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["TypeAccount"] = new SelectList(typeAccountList, "Value", "Text");
            ViewData["TypeCurrency"] = new SelectList(typeCurrencyList, "Value", "Text");

            if (id.HasValue) { //Validates if the parameter has data

                //A customer is obtained from the id
                var customer = await _customerService.GetByIdForAccount((int)id);
                var accountList = await _accountService.GetByIdForBankAccount((int) id);

                if (customer != null)
                    return View(new AccountOppeningViewModel { Customer = customer, AccountList = accountList });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(AccountOppeningViewModel oAccountOppeningViewModel, int personId) {

            string script;
            var account = oAccountOppeningViewModel.Account;

            //Manual validation of the account model
            var accountValidationContext = new ValidationContext(account, serviceProvider: null, items: null);
            var accountValidationResults = new List<ValidationResult>();
            bool accountIsValid = Validator.TryValidateObject(account, accountValidationContext,
                accountValidationResults, validateAllProperties: true);

            if (!accountIsValid || personId == 0) {
                ViewData["CustomerError"] = "*Debe de buscar un cliente";
                return View("AccountOppening");
            }

            var result = await _accountService.Create(account);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido abrir la cuenta, inténtelo más tarde.');</script>";

                ViewData["AlertError"] = script;
                return View("AccountOppening");
            }
            script = "<script>AlertSuccess('Cuenta creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("AccountOppening", new { id = personId });
        }
    }
}