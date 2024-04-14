using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface ITypeCurrencyRepository {
   
        public Task CreateAsync(TypeCurrency oTypeCurrency);
        public Task<ICollection<TypeCurrency>> GetAllAsync();
        public Task UpdateAsync(TypeCurrency oTypeCurrency);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}