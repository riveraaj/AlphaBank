using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Interfaces.Common.Services
{
    public interface IContractService
    {

        public Task<bool> LoanTypeCreate(LoanApplication oLoanApplication);

        public Task<List<ShowContractDto>> GetByLoanApplicationID(int id);
    }
}
