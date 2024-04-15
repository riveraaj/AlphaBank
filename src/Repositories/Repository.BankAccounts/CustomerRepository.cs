using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Repository.BankAccounts {
    public class CustomerRepository(AlphaBankDbContext context) : ICustomerRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Customer?> GetByPersonIdAsync(int id)
            => await _context.Customers.Include(x => x.Person)
                                            .ThenInclude(y => y.Phones)
                                       .Include(x => x.Person)
                                            .ThenInclude(y => y.Nationality)
                                       .Include(x => x.Person)
                                            .ThenInclude(y => y.MaritalStatus)
                                       .Include(x => x.Occupation)
                                       .Where(x => x.PersonId == id).FirstOrDefaultAsync();

        public async Task<ICollection<Customer>> GetAllAsync()
            => await _context.Customers.Include(x => x.Occupation)
                                       .Include(x => x.CustomerStatus)
                                       .Include(x => x.Person)
                                            .ThenInclude(p => p.Phones)
                                        .Include(x => x.Person)
                                            .ThenInclude(y => y.Nationality)
                                       .Include(x => x.Person)
                                            .ThenInclude(y => y.MaritalStatus)
                                       .ToListAsync();

        public async Task CreateAsync(Customer oCustomer)
            => await _context.Customers.AddAsync(oCustomer);

        public async Task UpdateAsync(Customer oCustomer) {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == oCustomer.Id);

            if (customer == null) return;
            customer.EmailAddress = oCustomer.EmailAddress;
            customer.AverageMonthlySalary = oCustomer.AverageMonthlySalary;
            customer.OccupationId = oCustomer.OccupationId;
            customer.CustomerStatusId = oCustomer.CustomerStatusId;
        }

        public async Task UpdateStatusAsync(int id, byte customerStatusId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            customer!.CustomerStatusId = customerStatusId;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null) return;

            customer.Status = false;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}