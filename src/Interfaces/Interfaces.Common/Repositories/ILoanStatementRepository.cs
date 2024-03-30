using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface ILoanStatementRepository
    {

        public Task<ICollection<LoanStatement>> GetAllAsync();

    }
}
