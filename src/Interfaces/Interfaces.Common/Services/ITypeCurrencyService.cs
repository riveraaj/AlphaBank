using Data.AlphaBank;

namespace Interfaces.Common.Services
{
    public interface ITypeCurrencyService
    {
        public Task<List<TypeCurrency>> GetAll();
    }
}
