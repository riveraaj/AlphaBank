using Data.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;

namespace Service.BankAccounts {
    public class OccupationService(IOccupationRepository occupationRepository)
                                    : IOccupationService {

        private readonly IOccupationRepository _occupationRepository
            = occupationRepository;
       
        public async Task<List<Occupation>> GetAll() {
            try {
                //Retrieve all Occupation asynchronously from the OccupationRepository.
                return (List<Occupation>) await _occupationRepository.GetAllAsync();
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }
    }
}