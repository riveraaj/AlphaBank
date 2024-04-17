using Data.AlphaBank.Enums;
using Dtos.AlphaBank.Common;
using Interfaces.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {

    [Authorize(Roles = "1, 3")]
    public class TypeCurrencyController(ITypeCurrencyService typeCurrencyService) : Controller {

        private readonly ITypeCurrencyService _typeCurrencyService = typeCurrencyService;

        public async Task<IActionResult> TypeCurrencyList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _typeCurrencyService.GetAllForShow());
        }

        public IActionResult CreateTypeCurrency()
           => PartialView("CreateTypeCurrency");

        public async Task<IActionResult> UpdateTypeCurrency(int? id)  {

            if (!id.HasValue) return PartialView("UpdateTypeCurrency");

            var typeCurrency = await _typeCurrencyService.GetById((int)id);

            if (typeCurrency == null) return PartialView("UpdateTypeCurrency");

            return PartialView("UpdateTypeCurrency", typeCurrency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTypeCurrencyDTO oCreateTypeCurrencyDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear la moneda, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeCurrencyList");
            }

            var result = await _typeCurrencyService.Create(oCreateTypeCurrencyDTO);

            if (!result)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido crear la moneda, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeCurrencyList");
            }
            script = "<script>AlertSuccess('Moneda creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeCurrencyList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateTypeCurrencyDTO oUpdateTypeCurrencyDTO)  {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar la moneda, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeCurrencyList");
            }

            var result = await _typeCurrencyService.Update(oUpdateTypeCurrencyDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar la moneda, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeCurrencyList");
            }
            script = "<script>AlertSuccess('Moneda actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeCurrencyList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar la moneda, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeCurrencyList");
            }

            var result = await _typeCurrencyService.Remove((int)id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar la moneda, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeCurrencyList");
            }
            script = "<script>AlertSuccess('Moneda eliminado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeCurrencyList");
        }
    }
}