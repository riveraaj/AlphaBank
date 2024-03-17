using System;

namespace Service.BankAccounts.Helpers
{
    internal class AccountNumGeneratorHelper
    {

        private static readonly Random rnd = new Random();

        public static string AccountNumberGenerator(int typeAccountId, int typeCurrencyId)
        {          
            var countryCode = "CR";
            var verificationDigit = "99";
            var reservedCharacter = "0";
            var bankCode = "000";
            var branchCode = "000";
            var accountType = typeAccountId.ToString().PadLeft(2, '0');
            var currencyType = typeCurrencyId.ToString().PadLeft(2, '0');

            var accountId = rnd.Next(1000000, 10000000);

            var accountNumber = countryCode + verificationDigit + reservedCharacter + bankCode + branchCode + accountType + currencyType + accountId;

            return accountNumber;
        }

    }
}
