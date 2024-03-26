using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {

    [Authorize(Roles = "1, 5")]
    public class BankAccountsController : Controller {

        public IActionResult AccountClosing() => View();

        public IActionResult AccountOppening() => View();
    }
}