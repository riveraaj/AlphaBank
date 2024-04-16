using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    [Authorize]
    public class TypeLoanController(ITypeLoanService typeLoanService) : Controller {

        private readonly ITypeLoanService _typeLoanService = typeLoanService;

        public async Task<IActionResult> TypeLoanList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _typeLoanService.GetAllForShow());
        }

        public ActionResult CreateTypeLoan()
            => PartialView("CreateTypeLoan");

        public async Task<IActionResult> UpdateTypeLoan(int? id) {

            if (!id.HasValue) return PartialView("UpdateTypeLoan");

            var typeLoan = await _typeLoanService.GetById((int)id);

            if (typeLoan == null) return PartialView("UpdateTypeLoan");

            return PartialView("UpdateTypeLoan", typeLoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTypeLoanDTO oCreateTypeLoanDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el tipo de prestamo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeLoanList");
            }

            var result = await _typeLoanService.Create(oCreateTypeLoanDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el tipo de préstamo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeLoanList");
            }
            script = "<script>AlertSuccess('Tipo de préstamo creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeLoanList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateTypeLoanDTO oUpdateTypeLoanDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el tipo de préstamo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeLoanList");
            }

            var result = await _typeLoanService.Update(oUpdateTypeLoanDTO);

            if (!result)  {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el tipo de préstamo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeLoanList");
            }
            script = "<script>AlertSuccess('Tipo de préstamo actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeLoanList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el tipo de préstamo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeLoanList");
            }

            var result = await _typeLoanService.Remove((int)id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el tipo de préstamo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeLoanList");
            }
            script = "<script>AlertSuccess('Tipo de préstamo eliminada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeLoanList");
        }
    }
}