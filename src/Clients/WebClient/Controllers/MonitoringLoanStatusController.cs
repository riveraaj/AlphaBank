using Interfaces.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class MonitoringLoanStatusController(ILoanService loanService) : Controller {

        private readonly ILoanService _loanService = loanService;

        public async Task<IActionResult> MonitoringLoanStatus() 
            => View(await _loanService.GetAllForContinueLoan());     
    }
}