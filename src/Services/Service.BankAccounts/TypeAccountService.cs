using Data.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;

namespace Service.BankAccounts
{
    public class TypeAccountService (ITypeAccountRepository typeAccountRepository)
                                       : ITypeAccountService {

        private readonly ITypeAccountRepository _typeAccountRepository
            = typeAccountRepository;

        public async Task<List<TypeAccount>> GetAll()
        {
            try
            {
                //Retrieve all TypeAccount asynchronously from the TypeAccountRepository.
                return (List<TypeAccount>) await _typeAccountRepository.GetAllAsync();
            }
            catch
            {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

    }
}
