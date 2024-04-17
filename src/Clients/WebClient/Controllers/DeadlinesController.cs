using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    [Authorize(Roles = "1, 3")]
    public class DeadlinesController(IDeadlineService deadlineSevice) : Controller {

        private readonly IDeadlineService _deadlineService = deadlineSevice;

        public async Task<IActionResult> DeadlinesList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _deadlineService.GetAllForShow());
        }

        public IActionResult CreateDeadline() {
            return PartialView("CreateDeadline");
        }

        public async Task<IActionResult> UpdateDeadline(int? id) {

            if(!id.HasValue) return PartialView("UpdateDeadline");

            var deadline = await _deadlineService.GetById((int)id);

            if(deadline == null) return PartialView("UpdateDeadline");

            return PartialView("UpdateDeadline", deadline);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDeadlineDTO oCreateDeadlineDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el plazo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("DeadlinesList");
            }

            var result = await _deadlineService.Create(oCreateDeadlineDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el plazo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("DeadlinesList");
            }
            script = "<script>AlertSuccess('Plazo creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("DeadlinesList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateDeadlineDTO oUpdateDeadlineDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el plazo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("DeadlinesList");
            }

            var result = await _deadlineService.Update(oUpdateDeadlineDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el plazo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("DeadlinesList");
            }
            script = "<script>AlertSuccess('Plazo actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("DeadlinesList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el plazo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("DeadlinesList");
            }

            var result = await _deadlineService.Remove((int)id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el plazo, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("DeadlinesList");
            }
            script = "<script>AlertSuccess('Plazo eliminado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("DeadlinesList");
        }
    }
}