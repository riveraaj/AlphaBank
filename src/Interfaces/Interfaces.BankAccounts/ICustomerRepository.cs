using Data.AlphaBank;

namespace Interfaces.BankAccounts {
    public interface ICustomerRepository  {

        public Task<ICollection<Customer>> GetAllAsync();

        public Task CreateAsync(Customer oCustomer);
    }
}