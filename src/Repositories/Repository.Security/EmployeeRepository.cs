using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security {
    public class EmployeeRepository(AlphaBankDbContext context) : IEmployeeRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Employee?> GetByIdAsync(int id)
            => await _context.Employees.Include(x => x.SalaryCategory)
                                       .Include(x => x.Person)
                                             .ThenInclude(p => p.Phones)
                                       .FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task UpdateAsync(Employee oEmployee) {
            try {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == oEmployee.Id)
                    ?? throw new NullReferenceException("Employee not found");

                employee.SalaryCategoryId = oEmployee.SalaryCategoryId;           
            }
            catch (SqlException e){
                throw new Exception("Database error", e);
            }
        }


        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new NullReferenceException("Employee not found");

                employee.Status = false;
            } catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}