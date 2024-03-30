using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts {
    public static class AccountMapper {

        public static Account MapAccount(CreateAccountDto oCreateAccountDto)
            => new() {
                CustomerId = (int) oCreateAccountDto.CustomerId!,
                TypeAccountId = (byte) oCreateAccountDto.TypeAccountId!,
                TypeCurrencyId = (byte) oCreateAccountDto.TypeCurrencyId!,
            };

        public static ShowAccountDto MapShowAccountDto(Account oAccount)
            => new() {
                AccountNumber = oAccount.Id,
                Balance = oAccount.Balance,
                DateOpening = oAccount.DateOpening,
                AccountType = oAccount.TypeAccount.Description,
                AccountCurrency = oAccount.TypeCurrency.Description,
                CustomerID = oAccount.CustomerId
            };

        public static ShowAccountForPersonDto MapShowAccountForPersonDto(Account oAccount)
            => new() {
                AccountId = oAccount.Id,
                State = (oAccount.Status) ? "Activa" : "Inactiva",
                TypeCurrency = oAccount.TypeCurrency.Description
            };
    }
}