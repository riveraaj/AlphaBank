using Data.AlphaBank;

namespace Interfaces.ContinueLoans.Repositories {
    public interface ICollectionHistoryRepository {
        public Task<ICollection<CollectionHistory>> GetAllAsync();
        public Task CreateAsync(CollectionHistory oCollectionHistory);
        public Task SaveChangesAsync();
    }
}