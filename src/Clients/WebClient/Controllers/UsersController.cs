using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Services;

namespace WebClient.Controllers {

    [Authorize(Roles = "1")]
    public class UsersController(IUserService userService,
                                 SecurityService securityService) : Controller  {

        private readonly IUserService _userService = userService;
        private readonly SecurityService _securityService = securityService;

        public async Task<IActionResult> UsersList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _userService.GetAll());
        }

        public async Task<ActionResult> CreateUsers() {
            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData

            var roleList = await _securityService.GetRoleSelectListItems();
            var employeeList = await _securityService.GetEmployeeSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["Role"] = new SelectList(roleList, "Value", "Text");
            ViewData["Employee"] = new SelectList(employeeList, "Value", "Text");

            return PartialView("CreateUsers");
        }
         
        public async Task<ActionResult> UpdateUser(int? id) {

            if(!id.HasValue) return PartialView("UpdateUser");

            var user = await _userService.GetById((int)id);

            if(user == null) return PartialView("UpdateUser");

            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var roleList = await _securityService.GetRoleSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["Role"] = new SelectList(roleList, "Value", "Text", roleList.FirstOrDefault(x => x.Value.Equals(user.RoleId)));

            return PartialView("UpdateUser", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserDTO oCreateUserDTO){
            string script;  

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el usuario, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("UsersList");
            }

            var result = await _userService.Create(oCreateUserDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el usuario, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("UsersList");
            }

            script = "<script>AlertSuccess('Usuario creado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("UsersList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateUserDTO oUpdateUserDTO) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el usuario, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("UsersList");
            }

            var result = await _userService.Update(oUpdateUserDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el usuario, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("UsersList");
            }

            script = "<script>AlertSuccess('Usuario actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("UsersList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id) {
            string script;

            if (!id.HasValue) {
                script = "<script>AlertError('Hubo un error','No se ha podido inactivar el usuario, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return PartialView("UsersList");
            }

            var result = await _userService.Remove((int)id);

            if (!result){
                script = "<script>AlertError('Hubo un error','No se ha podido inactivar el usuario, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("UsersList");
            }

            script = "<script>AlertSuccess('Usuario inactivado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("UsersList");
        }
    }
}