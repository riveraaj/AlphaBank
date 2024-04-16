using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface IInterestRepository {
        public Task<Interest?> GetById(int id);
        public Task CreateAsync(Interest oInterest);
        public Task<ICollection<Interest>> GetAllAsync();
        public Task UpdateAsync(Interest oInterest);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}