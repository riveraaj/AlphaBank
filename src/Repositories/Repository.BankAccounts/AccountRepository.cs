using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts
{
    public class AccountRepository (AlphaBankDbContext context) : IAccountRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Account>> GetAllAsync()
            => await _context.Accounts.ToListAsync();

        public async Task<bool> CheckIfExistsByAccountNumberAsync(string accountNumber)
            => await _context.Accounts.AnyAsync(x => x.Id.Equals(accountNumber));

        public async Task CreateAsync(Account oAccount)
            => await _context.Accounts.AddAsync(oAccount);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
