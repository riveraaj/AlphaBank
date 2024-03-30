using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface IDeadlineService {
        public Task<List<Deadline>> GetAll();
    }
}