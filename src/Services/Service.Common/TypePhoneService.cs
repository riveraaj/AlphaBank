using Data.AlphaBank;
using Interfaces.Common;
using Interfaces.Security;

namespace Service.Common {
    public class TypePhoneService(ITypePhoneRepository typePhoneRepository) 
                                    : ITypePhoneService {

        private readonly ITypePhoneRepository _typePhoneRepository 
            = typePhoneRepository;

        public async Task<List<TypePhone>> GetAll()  {
            try {
                //Retrieve all TypePhone asynchronously from the TypePhoneRepository.
                return (List<TypePhone>) await _typePhoneRepository.GetAllAsync();
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }
    }
}