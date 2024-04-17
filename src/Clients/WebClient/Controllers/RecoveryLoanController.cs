using Dtos.AlphaBank.Common;
using Interfaces.Common.Services;
using Interfaces.RecoveringLoansService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using WebClient.Services;

namespace WebClient.Controllers {

    [Authorize(Roles = "1, 3, 2")]
    public class RecoveryLoanController(ILoanService loanService,
                                        AnalyzeLoanApplicationService analyzeLoanApplicationService,
                                        IRecoveringLoansService recoveringLoansService) : Controller {

        private readonly ILoanService _loanService = loanService;
        private readonly IRecoveringLoansService _recoveringLoansService = recoveringLoansService;
        private readonly AnalyzeLoanApplicationService _analyzeLoanApplicationService
            = analyzeLoanApplicationService;

        public  async Task<IActionResult> RecoveryLoanList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _loanService.GetAllForPaymentLoan());
        }

        public async Task<ActionResult> CreateRenegotiation(int? id) {
            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var deadlineList = await _analyzeLoanApplicationService.GetDeadlineSelectListItems();
            var interestList = await _analyzeLoanApplicationService.GetInterestSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["Deadline"] = new SelectList(deadlineList, "Value", "Text");
            ViewData["Interest"] = new SelectList(interestList, "Value", "Text");

            if(id.HasValue) ViewBag.LoanId = id.Value;

            return PartialView("CreateRenegotiation");
        }

        public async Task<ActionResult> Create(int loanId, CreateRenegotiationContractDTO oCreateRenegotiationContractDTO) {
            string script;

            if(!ModelState.IsValid || loanId == 0){
                script = "<script>AlertError('Hubo un error','No se ha podido renegociar, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RecoveryLoanList");
            }

            oCreateRenegotiationContractDTO.LoanId = loanId;

            var result = await _recoveringLoansService.GenerateNewContract(oCreateRenegotiationContractDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido renegociar, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RecoveryLoanList");
            }

            script = "<script>AlertSuccess('Renegociación creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("RecoveryLoanList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Notification(string loanId, string amount) {

            string script;

            if (loanId.IsNullOrEmpty() || amount.IsNullOrEmpty()) {
                script = "<script>AlertError('Hubo un error','No se ha podido notificar, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RecoveryLoanList");
            }
            
            var result = await _recoveringLoansService.JudicialLoanProcess(int.Parse(loanId), decimal.Parse(amount));
            
            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido notificar, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RecoveryLoanList");
            }
            script = "<script>AlertSuccess('Notificacion enviada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("RecoveryLoanList");
        }
    }
}