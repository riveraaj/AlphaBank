using Data.AlphaBank;
using Interfaces.Common;

namespace Service.Common {
    public class TypeIdentificationService(ITypeIdentificationRepository typeIdentificationRepository)
                                            : ITypeIdentificationService {

        private readonly ITypeIdentificationRepository _typeIdentificationRepository 
            = typeIdentificationRepository;

        public async Task<List<TypeIdentification>> GetAll()  {
            try {
                //Retrieve all TypeIdentification asynchronously from the TypeIdentificationRepository.
                return (List<TypeIdentification>) await _typeIdentificationRepository.GetAllAsync();
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }    
        }
    }
}