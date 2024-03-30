using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface ITypeCurrencyRepository {

        public Task<ICollection<TypeCurrency>> GetAllAsync();
    }
}