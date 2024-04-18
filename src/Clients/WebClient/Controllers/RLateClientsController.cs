using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class RLateClientsController : Controller
    {
        public IActionResult LateClientsList()
        {
            return View();
        }
    }
}
