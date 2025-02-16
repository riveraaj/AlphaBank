﻿using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class R_AccountsClosedController(IAccountService accountService) : Controller {

        private readonly IAccountService _accountService = accountService;

        public async Task<IActionResult> AccountsClosedList()
            => View(await _accountService.GetAllClosedAccounts());      
    }
}