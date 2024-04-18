using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface IAccountService {
        public Task<bool> Create(CreateAccountDTO createAccountDTO);
        public Task<bool> Remove(string accountNumber);
        public Task<List<ShowAccountDTO>> GetAll();
        public Task<List<Account>> GetByIdForLoanApplication(int id);
        public Task<List<ShowAccountForPersonDTO>> GetByIdForBankAccount(int id);
        public Task<List<ShowAccountCreatedDTO>> GetAllCreatedAccounts();
    }
}