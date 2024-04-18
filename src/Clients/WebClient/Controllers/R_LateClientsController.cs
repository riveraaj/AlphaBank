using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class R_LateClientsController : Controller
    {
        public IActionResult LateClientsList()
        {
            return View();
        }
    }
}
