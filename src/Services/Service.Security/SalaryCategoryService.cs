using Data.AlphaBank;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;

namespace Service.Security {
    public class SalaryCategoryService(ISalaryCategoryRepository salaryCategoryRepository) 
                                        : ISalaryCategoryService{

        private readonly ISalaryCategoryRepository _salaryCategoryRepository
            = salaryCategoryRepository;

        public async Task<List<SalaryCategory>> GetAll() {
            try {
                //Attempt to retrieve all roles asynchronously from the RoleRepository.
                return (List<SalaryCategory>)await _salaryCategoryRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }
    }
}