using Data.AlphaBank;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface ITypeLoanService {
        public Task<List<TypeLoan>> GetAll();
    }
}