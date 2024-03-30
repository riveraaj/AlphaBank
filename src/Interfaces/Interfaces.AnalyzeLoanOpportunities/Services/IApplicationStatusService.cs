using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface IApplicationStatusService {
        public Task<List<ApplicationStatus>> GetAll();
    }
}