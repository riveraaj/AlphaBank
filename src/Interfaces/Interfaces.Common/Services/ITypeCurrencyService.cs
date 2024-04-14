using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Interfaces.Common.Services {
    public interface ITypeCurrencyService {
        public Task<bool> Create(CreateTypeCurrencyDTO oCreateTypeCurrencyDTO);
        public Task<List<TypeCurrency>> GetAll();
        public Task<bool> Update(UpdateTypeCurrencyDTO oUpdateTypeCurrencyDTO);
        public Task<bool> Remove(int id);
    }
}