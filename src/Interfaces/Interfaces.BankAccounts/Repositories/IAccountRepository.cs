using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories
{
    public interface IAccountRepository
    {

        public Task<ICollection<Account>> GetAllAsync();

        public Task CreateAsync(Account oAccount);

        public Task SaveChangesAsync();

    }
}
