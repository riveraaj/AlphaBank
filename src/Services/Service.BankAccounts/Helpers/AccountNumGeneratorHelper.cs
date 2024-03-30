namespace Service.BankAccounts.Helpers {
    internal class AccountNumGeneratorHelper {

        private static readonly Random rnd = new();

        public static string AccountNumberGenerator(int typeAccountId, int typeCurrencyId){          
            //Contry Code of the IBAN Account
            var countryCode = "CR";
            // Verification Digit Acording with ISO / IEC-7064 (Using MOD97-10 Algorithm), in this case for beta version is an assigned number.
            var verificationDigit = "99";
            //Reserved Character always puted after the Country Code and Verification Digit
            var reservedCharacter = "0";
            // Bank Code from the Bank
            var bankCode = "000";
            // Branch Code of the Account
            var branchCode = "000";

            //Customer account (In this case is the type of account id, currency id and a random unique number generated)

            //Type of account id
            var accountType = typeAccountId.ToString().PadLeft(2, '0');
            //Currency id
            var currencyType = typeCurrencyId.ToString().PadLeft(2, '0');
            //Random unique number generated
            var accountId = rnd.Next(1000000, 10000000);

            //Cancatenate the parts of the IBAN to get the total IBAN Number of the Account
            var accountNumber = countryCode + verificationDigit + reservedCharacter + bankCode 
                + branchCode + accountType + currencyType + accountId;

            return accountNumber;
        }
    }
}