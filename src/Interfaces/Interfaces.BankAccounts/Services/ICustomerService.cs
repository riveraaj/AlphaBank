using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface ICustomerService {
        public Task<bool> Create(CreateCustomerDto createCustomerDto);

        public Task<List<ShowCustomerDto>> GetAll();

        public Task<ShowCustomerLoanDto?> GetById(int id);
    }
}