using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface IAccountService {

        public Task<bool> Create(CreateAccountDto createAccountDto);

        public Task<bool> Remove(string accountNumber);

        public Task<List<ShowAccountDto>> GetAll();

        public Task<List<Account>> GetByIdForLoanApplication(int id);

        public Task<List<ShowAccountForPersonDto>> GetByIdForBankAccount(int id);
    }
}