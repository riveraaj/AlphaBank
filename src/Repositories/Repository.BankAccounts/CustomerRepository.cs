using Data.AlphaBank;
using Database.AlphaBank;

namespace Repository.BankAccounts {
    public class CustomerRepository(AlphaBankDbContext context) {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Customer oCustomer)
            => await _context.Customers.AddAsync(oCustomer);
    }
}