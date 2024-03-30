using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Repositories;

namespace Interfaces.BankAccounts.Services {
    public interface ICustomerService {
        public Task<bool> Create(CreateCustomerDto createCustomerDto);

        public Task<List<ShowCustomerDto>> GetAll();

        public Task<ShowCustomerLoanDto?> GetByIdForLoan(int id);

        public Task<ShowCustomerDto?> GetByIdForAccount(int id);
    }
}