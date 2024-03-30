using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts {
    public class CustomerStatusRepository(AlphaBankDbContext context) : ICustomerStatusRepository {
        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<CustomerStatus>> GetAllAsync()
          => await _context.CustomerStatuses.ToListAsync();
    }
}