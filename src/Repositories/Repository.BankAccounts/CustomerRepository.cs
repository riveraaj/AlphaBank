using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.Data.SqlClient;
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
            try {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == oCustomer.Id) 
                    ?? throw new InvalidOperationException("Customer not found.");

                customer.EmailAddress = oCustomer.EmailAddress;
                customer.AverageMonthlySalary = oCustomer.AverageMonthlySalary;
                customer.OccupationId = oCustomer.OccupationId;
                customer.CustomerStatusId = oCustomer.CustomerStatusId;
            } catch (SqlException e) {
                throw new Exception("Database Error", e);
            }          
        }

        public async Task UpdateStatusAsync(int id, byte customerStatusId) {
            try {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new InvalidOperationException("Customer not found.");

                customer!.CustomerStatusId = customerStatusId;
            } catch (Exception e) {
                throw new Exception("Database Error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new InvalidOperationException("No se encontró el cliente en la base de datos.");
                customer!.Status = false;
            } catch (Exception e){
                throw new Exception("Database Error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}