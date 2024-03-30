using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface ILoanRepository
    {

        public Task<Loan?> GetById(int id);

        public Task<Loan?> GetByLoanApplicationId(int id);

        public Task<ICollection<Loan>> GetByPersonId(int id);

        public Task<ICollection<Loan>> GetAllAsync();

        public Task CreateAsync(Loan oLoan);

        public Task UpdateLoanStatus(int id, byte loanStatementId);

        public Task SaveChangesAsync();

    }
}
