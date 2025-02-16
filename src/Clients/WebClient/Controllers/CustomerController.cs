﻿using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Services;

namespace WebClient.Controllers
{

    [Authorize(Roles = "1,5")]
    public class CustomerController(ICustomerService customerService,
                                    CommonService commonService) : Controller
    {

        private readonly ICustomerService _customerService = customerService;
        private readonly CommonService _commonService = commonService;

        public async Task<IActionResult> CustomerList()
        {
            if (TempData.TryGetValue("AlertError", out object? error)) ViewBag.AlertError = error;
            if (TempData.TryGetValue("AlertSuccess", out object? success)) ViewBag.AlertSuccess = success;

            return View(await _customerService.GetAll());
        }
        public async Task<ActionResult> ShowCustomer()
        {
            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var typeIdentificationList = await _commonService.GetTypeIdentificationSelectListItems();
            var nationalityList = await _commonService.GetNationalitySelectListItems();
            var maritalStatusList = await _commonService.GetMaritalStatusSelectListItems();
            var occupationList = await _commonService.GetOccupationSelectListItems();
            var typePhoneList = await _commonService.GetTypePhoneSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["TypeIdentification"] = new SelectList(typeIdentificationList, "Value", "Text");
            ViewData["Nationality"] = new SelectList(nationalityList, "Value", "Text");
            ViewData["MaritalStatus"] = new SelectList(maritalStatusList, "Value", "Text");
            ViewData["Occupation"] = new SelectList(occupationList, "Value", "Text");
            ViewData["TypePhone"] = new SelectList(typePhoneList, "Value", "Text");
            return PartialView("CreateCustomer");
        }

        public async Task<ActionResult> ShowCustomerUpdate(int? id)
        {

            if (!id.HasValue) return PartialView("UpdateCustomer");

            var customer = await _customerService.GetByIdForUpdate(id.Value);

            if (customer == null) return PartialView("UpdateCustomer");

            //We obtain all the lists of data to be used in the select forms formatted in
            //SelectList to enter them in a ViewData
            var statusList = _commonService.GetStatusSelectListItems();
            var occupationList = await _commonService.GetOccupationSelectListItems();

            //ViewData is created for each SelectList and formatted as follows
            ViewData["Status"] = new SelectList(statusList, "Value", "Text", occupationList.FirstOrDefault(x => x.Value.Equals(customer.CustomerStatusId)));
            ViewData["Occupation"] = new SelectList(occupationList, "Value", "Text", occupationList.FirstOrDefault(x => x.Value.Equals(customer.OccupationId)));

            return PartialView("UpdateCustomer", customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDTO oCreateCustomerDTO)
        {

            string script;

            if (!ModelState.IsValid)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el cliente, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("CustomerList");
            }

            var result = await _customerService.Create(oCreateCustomerDTO);

            if (!result)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido crear el cliente, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("CustomerList");
            }
            script = "<script>AlertSuccess('Cliente creada exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("CustomerList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateCustomerDTO oUpdateCustomerDTO)
        {

            string script;

            if (!ModelState.IsValid)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el cliente, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("CustomerList");
            }

            var result = await _customerService.Update(oUpdateCustomerDTO);

            if (!result)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido actualizar el cliente, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("CustomerList");
            }

            script = "<script>AlertSuccess('Cliente actualizado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("CustomerList");
        }

        public async Task<ActionResult> Delete(int id)
        {
            string script;

            if (!ModelState.IsValid)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido inactivar el cliente, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("CustomerList");
            }

            var result = await _customerService.Remove(id);

            if (!result)
            {
                script = "<script>AlertError('Hubo un error','No se ha podido inactivar el cliente, inténtelo más tarde.');</script>";

                TempData["AlertError"] = script;
                return RedirectToAction("CustomerList");
            }
            script = "<script>AlertSuccess('Cliente inactivado exitosamente','');</script>";

            TempData["AlertSuccess"] = script;
            return RedirectToAction("CustomerList");
        }
    }
}