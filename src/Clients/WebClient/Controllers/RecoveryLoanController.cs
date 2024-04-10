using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class RecoveryLoanController : Controller
    {

        [Authorize(Roles = "1")]
        public async Task<IActionResult> RecoveryLoanList()
        {

            return View();
        }


        public ActionResult showRecoveryLoan()
        {
            return PartialView("CreateRecoveryLoan");
        }
        public ActionResult createRenegotiation()
        {
            return PartialView("CreateRenegotiation");
        }
    }
}
