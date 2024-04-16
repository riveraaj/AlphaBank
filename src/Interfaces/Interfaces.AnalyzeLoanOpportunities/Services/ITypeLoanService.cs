using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface ITypeLoanService {
        public Task<UpdateTypeLoanDTO?> GetById(int id);
        public Task<List<ShowCatalogsDTO>> GetAllForShow();
        public Task<bool> Create(CreateTypeLoanDTO oCreateTypeLoanDTO);
        public Task<List<TypeLoan>> GetAll();
        public Task<bool> Update(UpdateTypeLoanDTO oUpdateTypeLoanDTO);
        public Task<bool> Remove(int id);
    }
}