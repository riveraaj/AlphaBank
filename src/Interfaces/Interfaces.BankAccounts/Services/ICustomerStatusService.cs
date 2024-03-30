using Data.AlphaBank;

namespace Interfaces.BankAccounts.Services {
    public interface ICustomerStatusService {

        public Task<List<CustomerStatus>> GetAll();
    }
}