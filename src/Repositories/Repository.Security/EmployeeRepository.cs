using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security {
    public class EmployeeRepository(AlphaBankDbContext context) : IEmployeeRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Employee>> GetAllAsync()
              => await _context.Employees.Include(x => x.Person)
                                       .Include(x => x.Person.Phones.FirstOrDefault())
                                       .Include(x => x.Users.FirstOrDefault())
                                       .Include(x => x.Users.FirstOrDefault()!.Role)
                                       .ToListAsync();

        public async Task CreateAsync(Employee oEmployee)
            => await _context.Employees.AddAsync(oEmployee);

    }
}