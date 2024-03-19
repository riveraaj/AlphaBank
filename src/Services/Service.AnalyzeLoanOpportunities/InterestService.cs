using Data.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;

namespace Service.AnalyzeLoanOpportunities {
    public class InterestService(IInterestRepository interestRepository) : IInterestService {

        private readonly IInterestRepository _interestRepository = interestRepository;

        public async Task<List<Interest>> GetAll() {
            try {
                //Attempt to retrieve all interest asynchronously from the RoleRepository.
                return (List<Interest>)await _interestRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }
    }
}