using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;
using Mapper.BankAccounts;
using Microsoft.Extensions.Logging;
using Service.BankAccounts.Helpers;
using System;

namespace Service.BankAccounts
{
    public class AccountService(IAccountRepository accountRepository/*,
                                ILogger<AccountService> logger*/) : IAccountService
    {

        public readonly IAccountRepository _accountRepository = accountRepository;
        //public readonly ILogger<AccountService> _logger = logger;

        public async Task<bool> Create(CreateAccountDto oCreateAccountDto)
        {
            try
            {             

                //Map CreateAccountDto to account object using AccountMapper.
                var account = AccountMapper.MapAccount(oCreateAccountDto);

                var accountExist = true;
                //Set Id (IBAN) of the account
                while (accountExist)
                {
                    account.Id = AccountNumGeneratorHelper.AccountNumberGenerator(account.TypeAccountId, account.TypeCurrencyId);
                    accountExist = await _accountRepository.CheckIfExistsByAccountNumberAsync(account.Id);
                }


                // Set the status of the account to true.
                account.Status = true;

                // Set initial balance of the account.
                account.Balance = 0;

                //Set the opening date of the account.
                account.DateOpening = DateOnly.FromDateTime(DateTime.UtcNow);

                //_logger.LogInformation("----- Create Account: Start the creation of an account registry");

                await _accountRepository.CreateAsync(account);
                await _accountRepository.SaveChangesAsync();

                //_logger.LogInformation("----- Create Account: Creation completed and saved successfully.");


                //Return true to indicate successful creation.
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError($"----- Create Account: An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, return false.
                return false;
            }
        }

    }
}
