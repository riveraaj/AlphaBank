using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class AnalyzeLoanOpportunitiesController : Controller {
        public IActionResult LoanApplication() => View();
    }
}