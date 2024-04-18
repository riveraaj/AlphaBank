using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class R_HistoryApprovedLoansController(ILoanApplicationService loanApplicationService) : Controller {

        private readonly ILoanApplicationService _loanApplicationService = loanApplicationService;

        public async Task<IActionResult> HistoryApprovedLoansList()
            => View(await _loanApplicationService.GetAllApprovedLoanApplication());
    }
}