using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories {
    public interface ITypeAccountRepository {

        public Task CreateAsync(TypeAccount oTypeAccount);
        public Task<ICollection<TypeAccount>> GetAllAsync();
        public Task UpdateAsync(TypeAccount oTypeAccount);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}