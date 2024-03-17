
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Dtos.AlphaBank.BankAccounts
{
    public class ShowAccountDto
    {
        public string Id { get; set; } = null!;

        public bool Status { get; set; }

        public decimal Balance { get; set; }
    }
}
