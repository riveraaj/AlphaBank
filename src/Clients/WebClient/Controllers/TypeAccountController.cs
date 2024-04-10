using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Services;
using WebClient.Models;
using Data.AlphaBank;
using Service.BankAccounts;

namespace WebClient.Controllers

{
    public class TypeAccountController() : Controller
    {

        

        [Authorize(Roles = "1")]
        public async Task<IActionResult> TypeAccountList()
        {


            
            return View();
        }


        public ActionResult showTypeAccount()
        {
            return PartialView("CreateTypeAccount");
        }


    }
}


