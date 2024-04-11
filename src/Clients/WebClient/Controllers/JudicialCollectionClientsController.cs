using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class JudicialCollectionClientsController : Controller
    {
        [Authorize(Roles = "1")]
        public async Task<IActionResult> JudicialCollectionClientsList()
        {

            return View();
        }


        public ActionResult createNotificationJudicialCollection()
        {
            return PartialView("CreateNotificationJudicialCollection");
        }
    }
}
