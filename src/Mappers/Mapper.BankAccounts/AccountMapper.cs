using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts {
    public static class AccountMapper {

        public static Account MapAccount(CreateAccountDTO oCreateAccountDTO)
            => new() {
                CustomerId = (int) oCreateAccountDTO.CustomerId!,
                TypeAccountId = (byte) oCreateAccountDTO.TypeAccountId!,
                TypeCurrencyId = (byte) oCreateAccountDTO.TypeCurrencyId!,
            };

        public static ShowAccountDTO MapShowAccountDTO(Account oAccount)
            => new() {
                AccountNumber = oAccount.Id,
                Balance = oAccount.Balance,
                DateOpening = oAccount.DateOpening,
                AccountType = oAccount.TypeAccount.Description,
                AccountCurrency = oAccount.TypeCurrency.Description,
                CustomerID = oAccount.CustomerId
            };

        public static ShowAccountForPersonDTO MapShowAccountForPersonDTO(Account oAccount)
            => new() {
                AccountId = oAccount.Id,
                State = (oAccount.Status) ? "Activa" : "Inactiva",
                TypeCurrency = oAccount.TypeCurrency.Description
            };
    }
}