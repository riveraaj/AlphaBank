using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common
{
    public static class TypeCurrencyMapper
    {

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
    }
}
