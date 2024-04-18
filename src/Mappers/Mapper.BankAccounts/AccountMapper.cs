using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;
using System.Globalization;

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

        public static ShowAccountCreatedDTO MapShowAccountCreatedDTO(Account oAccount)
            => new()
            {
                Id = oAccount.Id,
                TypeAccountDescription = oAccount.TypeAccount.Description,
                TypeCurrencyDescription = oAccount.TypeCurrency.Description,
                PersonId = oAccount.Customer.PersonId,
                Balance = oAccount.Balance.ToString("#,0", CultureInfo.InvariantCulture),
                DateOpening = oAccount.DateOpening
            };
    }
}