using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class AnalyzeLoanOpportunitiesController(ILoanApplicationService loanApplicationService) 
                                                        : Controller {

        private readonly ILoanApplicationService _loanApplicationService
            = loanApplicationService;

        public IActionResult LoanApplication() => View();

        public async Task<IActionResult> GetCustomer() {

            //var customer = _loanApplicationService.

            return View();
        }
    }
}