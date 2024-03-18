
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Dtos.AlphaBank.BankAccounts
{
    public class ShowAccountDto
    {
        public string AccountNumber { get; set; } = null!;

        public decimal Balance { get; set; }

        public DateOnly DateOpening { get; set; }

        public string AccountType { get; set; } = null!;

        public string AccountCurrency { get; set; } = null!;

        public int CustomerID { get; set; }

    }
}
