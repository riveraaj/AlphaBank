using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Common;

namespace Interfaces.Common.Services {
    public interface ITypeCurrencyService {
        public Task<UpdateTypeCurrencyDTO?> GetById(int id);
        public Task<List<ShowCatalogsDTO>> GetAllForShow();
        public Task<bool> Create(CreateTypeCurrencyDTO oCreateTypeCurrencyDTO);
        public Task<List<TypeCurrency>> GetAll();
        public Task<bool> Update(UpdateTypeCurrencyDTO oUpdateTypeCurrencyDTO);
        public Task<bool> Remove(int id);
    }
}