using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;

namespace Service.Common
{

    public class TypeContractService (ITypeContractRepository typeContractRepository)
                                        : ITypeContractService
    {
        private readonly ITypeContractRepository _typeContractRepository
            = typeContractRepository;

        public async Task<List<TypeContract>> GetAll()
        {
            try
            {
                //Retrieve all TypeContract asynchronously from the TypeContractRepository.
                return (List<TypeContract>) await _typeContractRepository.GetAllAsync();
            }
            catch
            {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

    }

}
