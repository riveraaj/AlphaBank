using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class R_AccountsCreatedController : Controller
    {
        public IActionResult AccountsCreatedList()
        {
            return View();
        }
    }
}
