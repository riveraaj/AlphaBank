using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface ITypeLoanRepository {

        public Task CreateAsync(TypeLoan oTypeLoan);
        public Task<ICollection<TypeLoan>> GetAllAsync();
        public Task UpdateAsync(TypeLoan oTypeLoan);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}