using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories { 
    public interface IAccountRepository {
        public Task<Account?> GetByIdForLoanApplication(string id);
        public Task<ICollection<Account>> GetByPersonIdForLoanApplication(int id);
        public Task<bool> CheckIfExistsByAccountNumberAsync(string accountNumber);
        public Task<ICollection<Account>> GetAllAsync();
        public Task CreateAsync(Account oAccount);
        public Task<bool> RemoveAsync(string accountNumber);
        public Task SaveChangesAsync();
    }
}