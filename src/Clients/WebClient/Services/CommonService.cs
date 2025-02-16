﻿using Data.AlphaBank.Enums;
using Interfaces.BankAccounts.Services;
using Interfaces.Common.Services;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Security;
using System.Globalization;

namespace WebClient.Services {
    public class CommonService(ITypeCurrencyService typeCurrencyService,
                               IOccupationService occupationService,
                               ITypePhoneService typePhoneService,
                               ITypeIdentificationService typeIdentificationService,
                               INationalityService nationalityService,
                               IMaritalStatusService maritalStatusService,
                               ISalaryCategoryService salaryCategoryService) {
                                
        private readonly ITypeCurrencyService _typeCurrencyService = typeCurrencyService;
        private readonly IOccupationService _occupationService = occupationService;
        private readonly ITypePhoneService _typePhoneService = typePhoneService;
        private readonly ITypeIdentificationService _typeIdentificationService = typeIdentificationService;
        private readonly INationalityService _nationalityService = nationalityService;
        private readonly IMaritalStatusService _maritalStatusService = maritalStatusService;
        private readonly ISalaryCategoryService _salaryCategoryService = salaryCategoryService;

        public async Task<IEnumerable<SelectListItem>> GetSalarySelectListItems()
        {

            var salaryList = await _salaryCategoryService.GetAll();

            return salaryList.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = "₡ " + x.Description.ToString("#,0", CultureInfo.InvariantCulture)
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypeCurrencySelectListItems() {

            var typeCurrencyList = await _typeCurrencyService.GetAll();

            return typeCurrencyList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public IEnumerable<SelectListItem> GetStatusSelectListItems()
            => Enum.GetValues(typeof(CustomerStatusEnum)).Cast<CustomerStatusEnum>()
                                                 .Select(x => new SelectListItem() {
                                                    Text = x.ToString(),
                                                    Value = ((int)x).ToString()
                                                 }).ToList();

        public async Task<IEnumerable<SelectListItem>> GetOccupationSelectListItems() {

            var occupationList = await _occupationService.GetAll();

            return occupationList.Select(x => new SelectListItem  {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypePhoneSelectListItems(){

            var typePhoneList = await _typePhoneService.GetAll();

            return typePhoneList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypeIdentificationSelectListItems(){

            var typeIdentificationList = await _typeIdentificationService.GetAll();

            return typeIdentificationList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetNationalitySelectListItems() {

            var nationalityList = await _nationalityService.GetAll();

            return nationalityList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetMaritalStatusSelectListItems() {

            var maritalStatusList = await _maritalStatusService.GetAll();

            return maritalStatusList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }
    }
}