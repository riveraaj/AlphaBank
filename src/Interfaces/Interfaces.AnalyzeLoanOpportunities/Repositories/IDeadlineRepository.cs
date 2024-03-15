using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface IDeadlineRepository {

        public Task<ICollection<Deadline>> GetAllAsync();
    }
}