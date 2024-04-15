using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories {
    public interface ICustomerRepository {
        public Task<Customer?> GetByPersonIdAsync(int id);
        public Task<ICollection<Customer>> GetAllAsync();
        public Task CreateAsync(Customer oCustomer);
        public Task UpdateAsync(Customer oCustomer);
        public Task UpdateStatusAsync(int id, byte customerStatusId);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}