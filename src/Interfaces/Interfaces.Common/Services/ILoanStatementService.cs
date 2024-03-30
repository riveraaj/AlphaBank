using Data.AlphaBank;

namespace Interfaces.Common.Services
{
    public interface ILoanStatementService
    {

        public Task<List<LoanStatement>> GetAll();

    }
}
