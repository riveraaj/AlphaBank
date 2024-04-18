using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class R_HistoryRejectedLoansController : Controller
    {
        public IActionResult HistoryRejectedLoansList()
        {
            return View();
        }
    }
}
