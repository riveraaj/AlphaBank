using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;

namespace Service.Common {
    public class NationalityService(INationalityRepository nationalityRepository) 
                                       : INationalityService {

        private readonly INationalityRepository _nationalityRepository
            = nationalityRepository;

        public async Task<List<Nationality>> GetAll() {
            try {
                //Retrieve all Nationality asynchronously from the NationalityRepository.
                return (List<Nationality>) await _nationalityRepository.GetAllAsync();
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }
    }
}