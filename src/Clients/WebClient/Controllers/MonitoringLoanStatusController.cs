using Interfaces.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {

    [Authorize(Roles = "1, 3, 6")]
    public class MonitoringLoanStatusController(ILoanService loanService) : Controller {

        private readonly ILoanService _loanService = loanService;

        public async Task<IActionResult> MonitoringLoanStatus() 
            => View(await _loanService.GetAllForContinueLoan());     
    }
}