using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.Common.Services
{
    public interface ILoanService
    {

        public Task<bool> Create(LoanApplication oLoanApplication);

    }
}
