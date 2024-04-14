using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Interfaces.BankAccounts.Services {
    public interface ITypeAccountService {
        public Task<bool> Create(CreateTypeAccountDTO oCreateTypeAccountDTO);
        public Task<List<TypeAccount>> GetAll();
        public Task<bool> Update(UpdateTypeAccountDTO oUpdateTypeAccountDTO);
        public Task<bool> Remove(int id);
    }
}