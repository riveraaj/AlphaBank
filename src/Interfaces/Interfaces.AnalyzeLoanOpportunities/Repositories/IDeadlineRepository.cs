using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface IDeadlineRepository {
        public Task<Deadline?> GetById(int id);
        public Task CreateAsync(Deadline oDeadline);
        public Task<ICollection<Deadline>> GetAllAsync();
        public Task UpdateAsync(Deadline oDeadline);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}