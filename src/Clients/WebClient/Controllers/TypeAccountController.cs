using Data.AlphaBank.Enums;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {

    [Authorize(Roles = "1, 3")]
    public class TypeAccountController(ITypeAccountService typeAccountService) : Controller {

        private readonly ITypeAccountService _typeAccountService = typeAccountService;

        public async Task<IActionResult> TypeAccountList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _typeAccountService.GetAllForShow());
        }

        public ActionResult CreateTypeAccount() {
            return PartialView("CreateTypeAccount");
        }

        public async Task<IActionResult> UpdateTypeAccount(int? id) {

            if (!id.HasValue) return PartialView("UpdateTypeAccount");

            var typeAccount = await _typeAccountService.GetById((int)id);

            if (typeAccount == null) return PartialView("UpdateTypeAccount");

            return PartialView("UpdateTypeAccount", typeAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTypeAccountDTO oCreateTypeAccountDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el tipo de cuenta, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeAccountList");
            }

            var result = await _typeAccountService.Create(oCreateTypeAccountDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el tipo de cuenta, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeAccountList");
            }
            script = "<script>AlertSuccess('Tipo de cuenta creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeAccountList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateTypeAccountDTO oUpdateTypeAccountDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el tipo de cuenta, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeAccountList");
            }

            var result = await _typeAccountService.Update(oUpdateTypeAccountDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el tipo de cuenta, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeAccountList");
            }
            script = "<script>AlertSuccess('Tipo de cuenta actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeAccountList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el tipo de cuenta, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeAccountList");
            }

            var result = await _typeAccountService.Remove((int)id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el tipo de cuenta, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("TypeAccountList");
            }
            script = "<script>AlertSuccess('Tipo de cuenta eliminada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("TypeAccountList");
        }
    }
}