using Data.AlphaBank;

namespace Interfaces.BankAccounts.Services
{
    public interface ITypeAccountService
    {

        public Task<List<TypeAccount>> GetAll();

    }
}
