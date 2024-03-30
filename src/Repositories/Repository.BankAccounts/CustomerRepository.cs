﻿using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

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
                                       .ToListAsync();

        public async Task CreateAsync(Customer oCustomer)
            => await _context.Customers.AddAsync(oCustomer);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}