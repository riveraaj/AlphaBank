using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class HomeController
        (ILogger<HomeController> logger, IUserAuthenticatorService userService,
        IEmployeeService employeeService) : Controller {

        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUserAuthenticatorService _userService = userService;
        private readonly IEmployeeService _employeeService = employeeService;

        public IActionResult Index()
             => View();

        [Authorize]
        public async Task<IActionResult> Privacy() {

            var employeeList = await _employeeService.GetAll();

            return View(employeeList);
        }
          
        

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()  {
            await CookiesService.RemoveAuthenticationCookie(HttpContext);
            return Redirect("~/Login/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}