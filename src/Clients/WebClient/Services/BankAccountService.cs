using Interfaces.BankAccounts.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Services {
    public class BankAccountService(ITypeAccountService typeAccountService) {

        private readonly ITypeAccountService _typeAccountService1 = typeAccountService;

        public async Task<IEnumerable<SelectListItem>> GetTypeAccountSelectListItems() {

            var typeAccountList = await _typeAccountService1.GetAll();

            return typeAccountList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }
    }
}