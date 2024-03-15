using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface ILoanApplication {

        public Task<ICollection<LoanApplication>> GetAllAsync();

        public Task Create(LoanApplication oLoanApplication);

        public Task UpdateApplicationStatus(int id);
    }
}