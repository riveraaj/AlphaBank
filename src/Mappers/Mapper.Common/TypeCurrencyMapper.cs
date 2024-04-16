using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Common;

namespace Mapper.Common {
    public static class TypeCurrencyMapper {

        public static ShowCatalogsDTO MapShowTypeCurrencyDTO(TypeCurrency oTypeCurrency)
           => new() {
               Id = oTypeCurrency.Id.ToString(),
               Description = oTypeCurrency.Description
           };

        public static TypeCurrency MapTypeCurrency(CreateTypeCurrencyDTO oCreateTypeCurrencyDTO)
            => new()
            {
                Description = oCreateTypeCurrencyDTO.Description
            };

        public static TypeCurrency MapUpdateTypeCurrency(UpdateTypeCurrencyDTO oUpdateTypeCurrencyDTO)
            => new()
            {
                Id = oUpdateTypeCurrencyDTO.Id,
                Description = oUpdateTypeCurrencyDTO.Description
            };

        public static UpdateTypeCurrencyDTO MapUpdateTypeCurrency(TypeCurrency oTypeCurrency)
            => new() {
                Id = oTypeCurrency.Id,
                Description = oTypeCurrency.Description
            };
    }
}
