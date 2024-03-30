using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories {
    public interface ICustomerStatusRepository {

        public Task<ICollection<CustomerStatus>> GetAllAsync();
    }
}