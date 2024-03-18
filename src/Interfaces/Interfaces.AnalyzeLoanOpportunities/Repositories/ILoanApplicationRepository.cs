using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface ILoanApplicationRepository {

        public Task<ICollection<LoanApplication>> GetAllAsync();

        public Task Create(LoanApplication oLoanApplication);

        public Task UpdateApplicationStatus(int id, byte statusId);

        public Task SaveChangesAsync();
    }
}