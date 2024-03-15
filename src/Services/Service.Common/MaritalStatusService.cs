using Data.AlphaBank;
using Interfaces.Common;

namespace Service.Common {
    public class MaritalStatusService(IMaritalStatusRepository maritalStatusRepository)
                                        : IMaritalStatusService{

        private readonly IMaritalStatusRepository _maritalStatusRepository 
            = maritalStatusRepository;

        public async Task<List<MaritalStatus>> GetAll() {
            try{
                //Retrieve all MaritalStatus asynchronously from the MaritalStatusRepository.
                return (List<MaritalStatus>) await _maritalStatusRepository.GetAllAsync();
            }
            catch  {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }
    }
}