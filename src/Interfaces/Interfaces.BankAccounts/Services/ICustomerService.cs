using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface ICustomerService {
        public Task<bool> Create(CreateCustomerDTO createCustomerDTO);
        public Task<List<ShowCustomerDTO>> GetAll();
        public Task<ShowCustomerLoanDTO?> GetByIdForLoan(int id);
        public Task<ShowCustomerDTO?> GetByIdForAccount(int id);
        public Task Update(int id, byte customerStatusId);
    }
}