using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories {
    public interface ITypeAccountRepository {
        public Task<ICollection<TypeAccount>> GetAllAsync();

    }
}