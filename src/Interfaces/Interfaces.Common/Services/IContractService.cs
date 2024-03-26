using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.Common.Services
{
    public interface IContractService
    {

        public Task<bool> LoanTypeCreate(LoanApplication oLoanApplication);

    }
}
