using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Repositories {
    public interface ITypeLoanRepository {
        public Task<ICollection<TypeLoan>> GetAllAsync();
    }
}