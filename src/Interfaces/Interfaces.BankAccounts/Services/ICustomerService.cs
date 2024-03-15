using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services
{
    public interface ICustomerService
    {
        public Task<bool> Create(CreateCustomerDto createCustomerDto);
    }
}