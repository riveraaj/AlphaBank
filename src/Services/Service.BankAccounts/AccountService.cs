using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;
using Mapper.BankAccounts;
using Microsoft.Extensions.Logging;
using Service.BankAccounts.Helpers;

namespace Service.BankAccounts {
    public class AccountService(IAccountRepository accountRepository,
                                ILogger<AccountService> logger) : IAccountService {

        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ILogger<AccountService> _logger = logger;

        public async Task<bool> Create(CreateAccountDTO oCreateAccountDTO) {
            try {             

                //Map CreateAccountDto to account object using AccountMapper.
                var account = AccountMapper.MapAccount(oCreateAccountDTO);

                var accountExist = true;
                //Set Id (IBAN) of the account
                while (accountExist) {
                    account.Id = AccountNumGeneratorHelper.AccountNumberGenerator(account.TypeAccountId, account.TypeCurrencyId);
                    accountExist = await _accountRepository.CheckIfExistsByAccountNumberAsync(account.Id);
                }


                // Set the status of the account to true.
                account.Status = true;

                // Set initial balance of the account.
                account.Balance = 0;

                //Set the opening date of the account.
                account.DateOpening = DateOnly.FromDateTime(DateTime.UtcNow);

                _logger.LogInformation("----- Create Account: Start the creation of an account registry");

                await _accountRepository.CreateAsync(account);
                await _accountRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Account: Creation completed and saved successfully.");


                //Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("----- Create Account: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<bool> Remove(string accountNumber) {
            try {
                _logger.LogInformation("----- Remove Account: Start the removement of an account");

                var result = await _accountRepository.RemoveAsync(accountNumber);

                if (!result) return false;

                await _accountRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove Account: Removement completed and saved successfully.");

                //Return true to indicate successful removement.
                return true;
            } catch {
                _logger.LogError("----- Remove Account: An error occurred while removing the account from the database.");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<ShowAccountDTO>> GetAll() {
            try {
                //Retrieve all accounts asynchronously from the AccountRepository.
                var accountList = await _accountRepository.GetAllAsync();

                //Initialize a list to store ShowAccountDto objects.
                var showAccountDtoList = new List<ShowAccountDTO>();

                //Map each account to a ShowAccountDto and add it to the list.
                foreach (var account in accountList)
                    showAccountDtoList.Add(AccountMapper.MapShowAccountDTO(account));

                // Return the list of ShowAccountDto objects.
                return showAccountDtoList;

            } catch {
                return [];
            }
        }

        public async Task<List<Account>> GetByIdForLoanApplication(int id){
            try {
                //Retrieve all accounts asynchronously from the AccountRepository.
                var accountList = await _accountRepository.GetByPersonIdForLoanApplication(id);

                // Return the list of ShowAccountDto objects.
                return accountList.Where(x => x.Status == true).ToList();
            } catch {
                return [];
            }
        }

        public async Task<List<ShowAccountForPersonDTO>> GetByIdForBankAccount(int id) {
            try {
                //Retrieve all accounts asynchronously from the AccountRepository.
                var accountList = await _accountRepository.GetByPersonIdForLoanApplication(id);

                //Initialize a list to store ShowAccountForPersonDto objects.
                var showAccountForPersonList = new List<ShowAccountForPersonDTO>();

                //Map each account to a ShowAccountForPersonDto and add it to the list.
                foreach (var account in accountList)
                    showAccountForPersonList.Add(AccountMapper.MapShowAccountForPersonDTO(account));

                // Return the list of ShowAccountForPersonDto objects.
                return showAccountForPersonList;

            } catch {
                return [];
            }
        }
    }
}