using Data.AlphaBank;

namespace Repository.ContinueLoans.Repositories {
    public interface ICollectionHistoryRepository {
        public Task<ICollection<CollectionHistory>> GetAllAsync();
        public Task CreateAsync(CollectionHistory oCollectionHistory);
        public Task SaveChangesAsync();
    }
}