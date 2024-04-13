using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Services;
using Interfaces.GrantingLoans.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers {
    [Authorize]
    public class GrantLoanController(ILoanApplicationService loanApplicationService,
                                     ICustomerService customerService,
                                     IGrantingLoansService grantingLoansService) : Controller {

        private readonly ILoanApplicationService _loanApplicationService 
            = loanApplicationService;

        private readonly ICustomerService _customerService = customerService;
        private readonly IGrantingLoansService _grantingLoansService = grantingLoansService;

        public async Task<IActionResult> EvaluationLoanApplications() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            var loanList = await _loanApplicationService.GetAllForGrantLoan();
            return View(new GrantLoanViewModel { LoanApplicationList = loanList});
        }

        public async Task<ActionResult> ViewApplication(int? id) {

            if (id.HasValue) {

                var loanList = await _loanApplicationService.GetAllForGrantLoan();
                var loan = await _loanApplicationService.GetById((int)id);
                var customer = await _customerService.GetByIdForLoan(loan!.PersonId);

                return PartialView("ViewApplication", new GrantLoanViewModel {
                    Customer = customer!,
                    LoanApplication = loan,
                    LoanApplicationList = loanList
                });
            } return PartialView("ViewApplication");
        }

        [HttpPost]
        public async Task<ActionResult> DenyLoanApplication(int id) {
            string script;

            if (id == 0) {
                TempData["CustomerError"] = "*Hubo un error al denegar la solicitud";
                return RedirectToAction("EvaluationLoanApplications");
            }

            var result = await _grantingLoansService.GrantingLoan(id, false);

            if(!result) {
                TempData["CustomerError"] = "*Hubo un error al denegar la solicitud";
                return RedirectToAction("EvaluationLoanApplications");
            }
            script = "<script>AlertSuccess('Solicitud rechazada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("EvaluationLoanApplications");
        }

        [HttpPost]
        public async Task<ActionResult> AcceptLoanApplication(int id){
            string script;

            if (id == 0){
                TempData["CustomerError"] = "*Hubo un error al denegar la solicitud";
                return RedirectToAction("EvaluationLoanApplications");
            }

            var result = await _grantingLoansService.GrantingLoan(id, true);

            if (!result) {
                TempData["CustomerError"] = "*Hubo un error al aprobar la solicitud";
                return RedirectToAction("EvaluationLoanApplications");
            }
            script = "<script>AlertSuccess('Solicitud aprobada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("EvaluationLoanApplications");
        }
    } 
}