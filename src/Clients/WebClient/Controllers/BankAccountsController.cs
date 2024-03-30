using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
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


        [HttpGet]
        public async Task<IActionResult> AccountClosing(int? id) {

            if (id.HasValue) { //Validates if the parameter has data

                //A customer is obtained from the id
                var customer = await _customerService.GetByIdForAccount((int)id);
                var accountList = await _accountService.GetByIdForBankAccount((int)id);

                if (customer != null)
                    return View(new AccountClosingViewModel { Customer = customer, AccountList = accountList });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AccountOppening(int? id){

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
        public async Task<IActionResult> CreateAccount(AccountOppeningViewModel oAccountViewModel, int personId) {

            var account = oAccountViewModel.Account;

            var result = await _accountService.Create(account);

            if (!result) {
                ViewData["Error"] = "*Hubo un error en la apertura de cuenta, intentelo más tarde.";
                return RedirectToAction("AccountOppening");
            }

            return RedirectToAction("AccountOppening", new { id = personId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseAccount(string accountId, int personId) {

            if (!accountId.IsNullOrEmpty()) {

                var result = await _accountService.Remove(accountId);

                if (!result) {
                    ViewData["Error"] = "*Hubo un error en el cierre de cuenta, intentelo más tarde.";
                    return RedirectToAction("AccountClosing");
                }
                return RedirectToAction("AccountClosing", new { id = personId });
            }
            return RedirectToAction("AccountClosing");
        }
    }
}