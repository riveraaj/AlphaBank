using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface ITypeAccountService {
        public Task<UpdateTypeAccountDTO?> GetById(int id);
        public Task<List<ShowCatalogsDTO>> GetAllForShow();
        public Task<bool> Create(CreateTypeAccountDTO oCreateTypeAccountDTO);
        public Task<List<TypeAccount>> GetAll();
        public Task<bool> Update(UpdateTypeAccountDTO oUpdateTypeAccountDTO);
        public Task<bool> Remove(int id);
    }
}