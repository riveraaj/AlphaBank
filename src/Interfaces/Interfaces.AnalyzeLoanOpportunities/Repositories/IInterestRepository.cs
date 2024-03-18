using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface IInterestRepository {

        public Task<ICollection<Interest>> GetAllAsync();
    }
}