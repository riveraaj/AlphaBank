using Data.AlphaBank;

namespace Interfaces.ContinueLoans.Repositories {
    public interface ICollectionHistoryRepository {
        public Task<ICollection<CollectionHistory>> GetAllAsync();
        public Task<List<CollectionHistory>> GetByLoanId(int id);
        public Task<CollectionHistory?> GetLastByLoanId(int id);
        public Task CreateAsync(CollectionHistory oCollectionHistory);
        public Task SaveChangesAsync();
    }
}