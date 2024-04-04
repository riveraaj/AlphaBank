using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers {

    [Authorize]
    public class AnalyzeLoanOpportunitiesController(ILoanApplicationService loanApplicationService,
                                                    ICustomerService customerService,
                                                    AnalyzeLoanApplicationService analyzeLoanApplicationService,
                                                    CommonService commonService) : Controller {

        private readonly ILoanApplicationService _loanApplicationService
            = loanApplicationService;

        private readonly ICustomerService _customerService = customerService;

        private readonly AnalyzeLoanApplicationService _analyzeLoanApplicationService
            = analyzeLoanApplicationService;

        private readonly CommonService _commonService = commonService;

        [HttpGet]
        public async Task<IActionResult> LoanApplication(int? id){

            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var typeLoanList = await _analyzeLoanApplicationService.GetTypeLoanSelectListItems();
            var deadlineList = await _analyzeLoanApplicationService.GetDeadlineSelectListItems();
            var typeCurrencyList = await _commonService.GetTypeCurrencySelectListItems();
            var interestList = await _analyzeLoanApplicationService.GetInterestSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["TypeLoan"] = new SelectList(typeLoanList, "Value", "Text");
            ViewData["Deadline"] = new SelectList(deadlineList, "Value", "Text");
            ViewData["TypeCurrency"] = new SelectList(typeCurrencyList, "Value", "Text");
            ViewData["Interest"] = new SelectList(interestList, "Value", "Text");

            if (id.HasValue) { //Validates if the parameter has data

                //A customer is obtained from the id
                var customer = await _customerService.GetByIdForLoan((int)id);

                if (customer != null) {
                    //The accounts related to the client are searched and saved in a
                    //ViewData to be shown in a select of the form.
                    var accountList = await _analyzeLoanApplicationService.GetAccountSelectListItems((int)id);
                    ViewData["Account"] = new SelectList(accountList, "Value", "Text");

                    return View(new LoanApplicationViewModel { Customer = customer });
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoanApplicationViewModel oLoanApplicationViewModel, List<IFormFile> files){

            string script;

            oLoanApplicationViewModel.LoanApplication.EmployerOrder
                = await _analyzeLoanApplicationService.ConvertFileUploadDTO(files[0]);

            oLoanApplicationViewModel.LoanApplication.SalaryStatement
                = await _analyzeLoanApplicationService.ConvertFileUploadDTO(files[1]);

            var loanApplication = oLoanApplicationViewModel.LoanApplication;

            //Manual validation of the loanApplication model
            var loanApplicationValidationContext = new ValidationContext(loanApplication, serviceProvider: null, items: null);
            var loanApplicationValidationResults = new List<ValidationResult>();
            bool loanApplicationIsValid = Validator.TryValidateObject(loanApplication, loanApplicationValidationContext,
                loanApplicationValidationResults, validateAllProperties: true);

            if (!loanApplicationIsValid) {
                ViewData["CustomerError"] = "*Debe de buscar un cliente";
                return View("LoanApplication");
            }

            var result = await _loanApplicationService.Create(loanApplication);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear la solicitud, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("LoanApplication");
            }
            script = "<script>AlertSuccess('Solicitud creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("LoanApplication"); ;
        }   
    }
}