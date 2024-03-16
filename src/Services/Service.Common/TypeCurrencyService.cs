
using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;

namespace Service.Common
{
    public class TypeCurrencyService (ITypeCurrencyRepository typeCurrencyRepository)
                                        : ITypeCurrencyService {

        private readonly ITypeCurrencyRepository _typeCurrencyRepository
            = typeCurrencyRepository;

        public async Task<List<TypeCurrency>> GetAll()
        {
            try
            {
                //Retrieve all TypeCurrency asynchronously from the TypeCurrencyRepository.
                return (List<TypeCurrency>) await _typeCurrencyRepository.GetAllAsync();
            }
            catch
            {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

    }
}
