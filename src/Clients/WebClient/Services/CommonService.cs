using Interfaces.Common.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Services {
    public class CommonService(ITypeCurrencyService typeCurrencyService) {

        private readonly ITypeCurrencyService _typeCurrencyService = typeCurrencyService;

        public async Task<IEnumerable<SelectListItem>> GetTypeCurrencySelectListItems() {

            var typeCurrencyList = await _typeCurrencyService.GetAll();

            return typeCurrencyList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }
    }
}