using Data.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;

namespace Service.AnalyzeLoanOpportunities {
    public class ApplicationStatusService(IApplicationStatusRepository applicationStatusRepository) {

        private readonly IApplicationStatusRepository _applicationStatusRepository
            = applicationStatusRepository;

        public async Task<List<ApplicationStatus>> GetAll() {
            try {
                //Attempt to retrieve all application status asynchronously from the RoleRepository.
                return (List<ApplicationStatus>)await _applicationStatusRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }
    }
}