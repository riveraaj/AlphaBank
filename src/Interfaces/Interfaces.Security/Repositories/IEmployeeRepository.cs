using Data.AlphaBank;

namespace Interfaces.Security.Repositories {
    public interface IEmployeeRepository {
        public Task<Employee?> GetByIdAsync(int id);
        public Task<ICollection<Employee>> GetAllAsync();
        public Task<Employee?> GetLastEmployeeAsync();
        public Task CreateAsync(Employee oEmployee);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}