using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class R_AccountsCreatedController(IAccountService accountService) : Controller {

        private readonly IAccountService _accountService = accountService;   

        public async Task<IActionResult> AccountsCreatedList()
            => View(await _accountService.GetAllCreatedAccounts());
    }
}