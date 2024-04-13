using Data.AlphaBank;
using Dtos.AlphaBank.ContinueLoans;

namespace Interfaces.Common.Services {
    public interface ILoanService {
        public Task<List<Loan>> GetAll();
        public Task<List<ShowLoanStatementDTO>> GetAllForContinueLoan();
        public Task<bool> Create(LoanApplication oLoanApplication);
        public Task UpdateStatement(int id, byte loanStatementId);
        public Task UpdateQuotas(int id);
        public Task<Loan?> GetById(int id);
    }
}