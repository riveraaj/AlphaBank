using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts {
    public class CustomerRepository(AlphaBankDbContext context) : ICustomerRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Customer?> GetByPersonIdAsync(int id)
            => await _context.Customers.Where(x => x.PersonId == id).FirstAsync();

        public async Task<ICollection<Customer>> GetAllAsync()
            => await _context.Customers.ToListAsync();

        public async Task CreateAsync(Customer oCustomer)
            => await _context.Customers.AddAsync(oCustomer);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}