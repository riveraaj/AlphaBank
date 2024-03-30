using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface IApplicationStatusRepository {
        public Task<ICollection<ApplicationStatus>> GetAllAsync();
    }
}