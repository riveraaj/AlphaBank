using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class UsersController : Controller
    {

        [Authorize(Roles = "1")]
        public async Task<IActionResult> UsersList()
        {

            return View();
        }


        public ActionResult createUsers()
        {
            return PartialView("CreateUsers");
        }
    }
}
