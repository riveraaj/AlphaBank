using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts {
    public interface ICustomerService {
        public Task<bool> Create(CreateCustomerDto createCustomerDto);

        public Task<List<Occupation>> GetAllOccupations();

        public Task<List<CustomerStatus>> GetAllCustomerStatuses();
    }
}