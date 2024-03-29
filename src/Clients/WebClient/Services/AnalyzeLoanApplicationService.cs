using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Services;
using Interfaces.Common.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Services {
    public class AnalyzeLoanApplicationService(ITypeLoanService typeLoanService,
                                        IDeadlineService deadlineService,
                                        ITypeCurrencyService typeCurrencyService,
                                        IInterestService interestService,
                                        IAccountService accountService) {

        private readonly ITypeLoanService _typeLoanService = typeLoanService;
        private readonly IDeadlineService _deadlineService = deadlineService;
        private readonly ITypeCurrencyService _typeCurrencyService = typeCurrencyService;
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

        public async Task<IEnumerable<SelectListItem>> GetTypeCurrencySelectListItems(){

            var typeCurrencyList = await _typeCurrencyService.GetAll();

            return typeCurrencyList.Select(x => new SelectListItem {
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
    }
}