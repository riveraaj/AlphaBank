using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IEmployeeRepository {

        public Task<ICollection<Employee>> GetAllAsync();
        public Task CreateAsync(Employee oEmployee);
    }
}