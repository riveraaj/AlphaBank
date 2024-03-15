using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts {
    public interface ICustomerService {
        public Task<bool> Create(CreateCustomerDto createCustomerDto);
    }
}