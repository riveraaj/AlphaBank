using Data.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;

namespace Service.AnalyzeLoanOpportunities {
    public class DeadlineService(IDeadlineRepository deadlineRepository) : IDeadlineService {

        private readonly IDeadlineRepository _deadlineRepository = deadlineRepository;

        public async Task<List<Deadline>> GetAll() {
            try {
                //Attempt to retrieve all deadline asynchronously from the RoleRepository.
                return (List<Deadline>)await _deadlineRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }
    }
}