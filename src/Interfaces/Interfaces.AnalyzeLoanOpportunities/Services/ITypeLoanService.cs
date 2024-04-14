using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Security;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface ITypeLoanService {
        public Task<bool> Create(CreateTypeLoanDTO oCreateTypeLoanDTO);
        public Task<List<TypeLoan>> GetAll();
        public Task<bool> Update(UpdateTypeLoanDTO oUpdateTypeLoanDTO);
        public Task<bool> Remove(int id);
    }
}