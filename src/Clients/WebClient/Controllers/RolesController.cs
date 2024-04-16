using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    [Authorize]
    public class RolesController(IRoleService roleService) : Controller {

        private readonly IRoleService _roleService = roleService;

        public async Task<IActionResult> RolesList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _roleService.GetAllForShow());
        }

        public ActionResult CreateRole()
            => PartialView("CreateRole");

        public async Task<IActionResult> UpdateRole(int? id) {

            if (!id.HasValue) return PartialView("UpdateRole");

            var Role = await _roleService.GetById((int)id);

            if (Role == null) return PartialView("UpdateRole");

            return PartialView("UpdateRole", Role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleDTO oCreateRoleDTO)  {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el rol, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RolesList");
            }

            var result = await _roleService.Create(oCreateRoleDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el rol, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RolesList");
            }
            script = "<script>AlertSuccess('Rol creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("RolesList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateRoleDTO oUpdateRoleDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el rol, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RolesList");
            }

            var result = await _roleService.Update(oUpdateRoleDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el rol, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RolesList");
            }
            script = "<script>AlertSuccess('Rol actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("RolesList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el Rol, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RolesList");
            }

            var result = await _roleService.Remove((int)id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido eliminar el rol, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("RolesList");
            }
            script = "<script>AlertSuccess('Rol eliminado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("RolesList");
        }
    }
}