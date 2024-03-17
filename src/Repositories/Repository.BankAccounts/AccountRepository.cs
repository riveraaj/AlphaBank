﻿using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts
{
    public class AccountRepository (AlphaBankDbContext context) : IAccountRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<bool> CheckIfExistsByAccountNumberAsync(string accountNumber)
            => await _context.Accounts.AnyAsync(x => x.Id.Equals(accountNumber));

        public async Task<ICollection<Account>> GetAllAsync()
            => await _context.Accounts.ToListAsync();

        public async Task<Account> GetByAccountNumberAsync(string accountNumber)
            => await _context.Accounts.SingleAsync(x => x.Id == accountNumber);

        public async Task<ICollection<Account>> GetByCustomerIdAsync(int customerId)
            => await _context.Accounts
                              .Where(x => x.CustomerId == customerId)
                              .ToListAsync();

        public async Task CreateAsync(Account oAccount)
            => await _context.Accounts.AddAsync(oAccount);

        public async Task RemoveAsync(string accountNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountNumber);

            if (account == null)
                return;


            if (account.Balance != 0.0m)
                return;

            account.Status = false;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

    }
}
