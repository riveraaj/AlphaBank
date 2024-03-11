using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security;

namespace Repository.Security {
    public class EmployeeRepository(AlphaBankDbContext context) : IEmployeeRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Employee oEmployee)
            => await _context.Employees.AddAsync(oEmployee);
    }
}