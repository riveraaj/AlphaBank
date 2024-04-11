using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    [Authorize]
    public class GrantLoanController(ILoanApplicationService loanApplicationService) : Controller {

        private readonly ILoanApplicationService _loanApplicationService 
            = loanApplicationService;

        public async Task<IActionResult> EvaluationLoanApplications()
            => View(await _loanApplicationService.GetAllForGrantLoan());
        public ActionResult viewApplication()
        {
            return PartialView("ViewApplication");
        }

    }
    
}