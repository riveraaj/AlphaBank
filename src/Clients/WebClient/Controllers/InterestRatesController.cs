using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class InterestRatesController(IInterestService interestService) : Controller {

        private readonly IInterestService _interestService = interestService;

        public async Task<IActionResult> InterestList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _interestService.GetAllForShow());
        }

        public IActionResult CreateInterest() 
            => PartialView("CreateInterest");
        
        public async Task<IActionResult> UpdateInterest(int? id) {

            if (!id.HasValue) return PartialView("UpdateInterest");

            var interest = await _interestService.GetById((int)id);

            if (interest == null) return PartialView("UpdateInterest");

            return PartialView("UpdateInterest", interest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateInterestDTO oCreateInterestDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el interés, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("InterestList");
            }

            var result = await _interestService.Create(oCreateInterestDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el interés, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("InterestList");
            }
            script = "<script>AlertSuccess('Interés creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("InterestList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateInterestDTO oUpdateInterestDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el interés, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("InterestList");
            }

            var result = await _interestService.Update(oUpdateInterestDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el interés, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("InterestList");
            }
            script = "<script>AlertSuccess('Interés actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("InterestList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el interés, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("InterestList");
            }

            var result = await _interestService.Remove((int)id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el interés, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("InterestList");
            }
            script = "<script>AlertSuccess('Interés eliminado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("InterestList");
        }
    }
}