using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security
{
    public class EmployeeRepository(AlphaBankDbContext context) : IEmployeeRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Employee?> GetByIdAsync(int id)
            => await _context.Employees.FindAsync(id);

        public async Task<ICollection<Employee>> GetAllAsync()
              => await _context.Employees.Include(x => x.Person)
                                             .ThenInclude(p => p.Phones)
                                         .Include(x => x.Users)
                                             .ThenInclude(u => u.Role)
                                         .ToListAsync();

        public async Task<Employee?> GetLastEmployeeAsync() {
            var employeeList = await GetAllAsync();
            return employeeList.Last();
        }

        public async Task CreateAsync(Employee oEmployee)
            => await _context.Employees.AddAsync(oEmployee);

        public async Task RemoveAsync(int id) {

            //Search for the record in the table 
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null) _context.Employees.Remove(employee);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

    }
}