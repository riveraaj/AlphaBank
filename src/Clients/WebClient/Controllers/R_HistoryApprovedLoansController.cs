using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class R_HistoryApprovedLoansController : Controller
    {
        public IActionResult HistoryApprovedLoansList()
        {
            return View();
        }
    }
}
