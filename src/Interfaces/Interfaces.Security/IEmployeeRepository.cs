using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IEmployeeRepository {

        public Task CreateAsync(Employee oEmployee);
    }
}