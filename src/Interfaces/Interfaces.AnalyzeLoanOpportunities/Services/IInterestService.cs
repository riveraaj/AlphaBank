using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface IInterestService {

        public Task<List<Interest>> GetAll();
    }
}