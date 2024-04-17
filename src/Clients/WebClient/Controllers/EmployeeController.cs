using Dtos.AlphaBank.Security;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Services;

namespace WebClient.Controllers {
    [Authorize(Roles = "1")]
    public class EmployeeController(IEmployeeService employeeService,
                                    CommonService commonService,
                                    SecurityService securityService) : Controller {

        private readonly IEmployeeService _employeeService = employeeService;
        private readonly CommonService _commonService = commonService;
        private readonly SecurityService _securityService = securityService;

        public async Task<IActionResult> EmployeeList() {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _employeeService.GetAll());
        }

        public async Task<IActionResult> CreateEmployee() {
            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var typeIdentificationList = await _commonService.GetTypeIdentificationSelectListItems();
            var nationalityList = await _commonService.GetNationalitySelectListItems();
            var maritalStatusList = await _commonService.GetMaritalStatusSelectListItems();
            var salaryList = await _securityService.GetSalarySelectListItems();
            var typePhoneList = await _commonService.GetTypePhoneSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["TypeIdentification"] = new SelectList(typeIdentificationList, "Value", "Text");
            ViewData["Nationality"] = new SelectList(nationalityList, "Value", "Text");
            ViewData["MaritalStatus"] = new SelectList(maritalStatusList, "Value", "Text");
            ViewData["Salary"] = new SelectList(salaryList, "Value", "Text");
            ViewData["TypePhone"] = new SelectList(typePhoneList, "Value", "Text");

            return PartialView("CreateEmployee");
        }

        public async Task<ActionResult> UpdateEmployee(int? id) {

            if (!id.HasValue) return PartialView("UpdateEmployee");

            var employee = await _employeeService.GetByIdForUpdate(id.Value);

            if (employee == null) return PartialView("UpdateEmployee");

            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var salaryList = await _securityService.GetSalarySelectListItems();
            var typePhoneList = await _commonService.GetTypePhoneSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["Salary"] = new SelectList(salaryList, "Value", "Text");
            ViewData["TypePhone"] = new SelectList(typePhoneList, "Value", "Text");

            return PartialView("UpdateEmployee", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDTO oCreateEmployeeDTO) {

            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el empleado, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("EmployeeList");
            }

            var result = await _employeeService.Create(oCreateEmployeeDTO);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el empleado, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("EmployeeList");
            }
            script = "<script>AlertSuccess('Empleado creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("EmployeeList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateEmployeeDTO oUpdateEmployeeDTO)  {

            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el empleado, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("EmployeeList");
            }

            var result = await _employeeService.Update(oUpdateEmployeeDTO);

            if (!result){
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el empleado, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("EmployeeList");
            }

            script = "<script>AlertSuccess('Empleado actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("EmployeeList");
        }

        public async Task<ActionResult> Delete(int id) {
            string script;

            if (!ModelState.IsValid) {
                script = "<script>AlertError('Hubo un error','No se ha podido inactivar el empleado, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("EmployeeList");
            }

            var result = await _employeeService.Remove(id);

            if (!result) {
                script = "<script>AlertError('Hubo un error','No se ha podido inactivar el empleado, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("EmployeeList");
            }
            script = "<script>AlertSuccess('Empleado inactivado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("EmployeeList");
        }
    }
}