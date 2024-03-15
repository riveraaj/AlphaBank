using Data.AlphaBank;

namespace Interfaces.BankAccounts {
    public interface ICustomerRepository  {

        public Task<Customer?> GetByPersonIdAsync(int id);

        public Task<ICollection<Customer>> GetAllAsync();

        public Task CreateAsync(Customer oCustomer);

        public Task SaveChangesAsync();
    }
}