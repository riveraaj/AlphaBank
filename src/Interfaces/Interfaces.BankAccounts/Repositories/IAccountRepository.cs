﻿using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories
{
    public interface IAccountRepository
    {
        public Task<bool> CheckIfExistsByAccountNumberAsync(string accountNumber);

        public Task<ICollection<Account>> GetAllAsync();

        public Task<Account> GetByAccountNumberAsync(string accountNumber);

        public Task<ICollection<Account>> GetByCustomerIdAsync(int customerId);

        public Task CreateAsync(Account oAccount);

        public Task RemoveAsync(string accountNumber);

        public Task SaveChangesAsync();

    }
}
