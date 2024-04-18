using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class R_HistoryRejectedLoansController(ILoanApplicationService loanApplicationService) : Controller {

        private readonly ILoanApplicationService _loanApplicationService = loanApplicationService;

        public async Task<IActionResult> HistoryRejectedLoansList()
         => View(await _loanApplicationService.GetAllRejectedLoanApplication());
    }
}