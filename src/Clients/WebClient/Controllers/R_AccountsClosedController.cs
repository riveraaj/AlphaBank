using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class R_AccountsClosedController : Controller
    {
        public IActionResult AccountsClosedList()
        {
            return View();
        }
    }
}
