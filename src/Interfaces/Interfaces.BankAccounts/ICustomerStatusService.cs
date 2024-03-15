using Data.AlphaBank;

namespace Interfaces.BankAccounts {
    public interface ICustomerStatusService {

        public Task<List<CustomerStatus>> GetAll();
    }
}