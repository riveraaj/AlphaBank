﻿using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Services {
    public class AnalyzeLoanApplicationService(ITypeLoanService typeLoanService,
                                        IDeadlineService deadlineService,
                                        IInterestService interestService,
                                        IAccountService accountService) {

        private readonly ITypeLoanService _typeLoanService = typeLoanService;
        private readonly IDeadlineService _deadlineService = deadlineService; 
        private readonly IInterestService _interestService = interestService;
        private readonly IAccountService _accountService = accountService;

        public async Task<IEnumerable<SelectListItem>> GetTypeLoanSelectListItems() {

            var typeLoanList = await _typeLoanService.GetAll();

            return typeLoanList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetDeadlineSelectListItems(){

            var deadlineList = await _deadlineService.GetAll();

            return deadlineList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetInterestSelectListItems() {

            var interestList = await _interestService.GetAll();

            return interestList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = $"{x.Description}  %"
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetAccountSelectListItems(int id) {

            var accountList = await _accountService.GetByIdForLoanApplication(id);

            return accountList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = $"{x.Id} - {x.TypeCurrency.Description}"
            });
        }

        public async Task<FileUploadDTO> ConvertFileUploadDTO(IFormFile file) {

            using var ms = new MemoryStream();

            await file.CopyToAsync(ms); //Copy file contents to MemoryStream

            var content = ms.ToArray(); //Convertir MemoryStream a byte[]

            var fileDTO = new FileUploadDTO { //Create DTO with file data
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileContent = content
            };

            return fileDTO;
        }
    }
}