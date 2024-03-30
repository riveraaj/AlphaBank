using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers {

    [Authorize]
    public class AnalyzeLoanOpportunitiesController(//ILoanApplicationService loanApplicationService,
                                                    ICustomerService customerService,
                                                    AnalyzeLoanApplicationService analyzeLoanApplicationService,
                                                    CommonService commonService) : Controller {

        //private readonly ILoanApplicationService _loanApplicationService
            //= loanApplicationService;

        private readonly ICustomerService _customerService = customerService;

        private readonly AnalyzeLoanApplicationService _analyzeLoanApplicationService
            = analyzeLoanApplicationService;

        private readonly CommonService _commonService = commonService;

        [HttpGet]
        public async Task<IActionResult> LoanApplication(int? id) {

            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var typeLoanList = await _analyzeLoanApplicationService.GetTypeLoanSelectListItems();
            var deadlineList = await _analyzeLoanApplicationService.GetDeadlineSelectListItems();
            var typeCurrencyList = await _commonService.GetTypeCurrencySelectListItems();
            var interestList = await _analyzeLoanApplicationService.GetInterestSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["TypeLoan"] = new SelectList(typeLoanList, "Value", "Text");
            ViewData["Deadline"] = new SelectList(deadlineList, "Value", "Text");
            ViewData["TypeCurrency"] = new SelectList(typeCurrencyList, "Value", "Text");
            ViewData["Interest"] = new SelectList(interestList, "Value", "Text");

            if (id.HasValue) { //Validates if the parameter has data

                //A customer is obtained from the id
                var customer = await _customerService.GetByIdForLoan((int) id);

                if (customer != null) {
                    //The accounts related to the client are searched and saved in a
                    //ViewData to be shown in a select of the form.
                    var accountList = await _analyzeLoanApplicationService.GetAccountSelectListItems((int)id);
                    ViewData["Account"] = new SelectList(accountList, "Value", "Text");

                    return View(new LoanApplicationViewModel { Customer = customer });
                }
            }

            return View();
        }
    }
}