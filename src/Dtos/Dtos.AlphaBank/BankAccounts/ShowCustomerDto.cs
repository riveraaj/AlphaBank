
using Dtos.AlphaBank.Common;

namespace Dtos.AlphaBank.BankAccounts
{
    public class ShowCustomerDto
    {

        public bool Status { get; set; }

        public string EmailAddress { get; set; } = null!;

        public decimal AverageMonthlySalary { get; set; }

        public string Occupation { get; set; } = null!;

        public string CustomerStatus { get; set; } = null!;

        public ShowPersonDto Person { get; set; } = null!;

    }
}
