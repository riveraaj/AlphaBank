using Data.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;

namespace Service.AnalyzeLoanOpportunities {
    public class TypeLoanService(ITypeLoanRepository typeLoanRepository) {

        private readonly ITypeLoanRepository _typeLoanRepository = typeLoanRepository;

        public async Task<List<TypeLoan>> GetAll() {
            try {
                //Attempt to retrieve all typeLoan asynchronously from the RoleRepository.
                return (List<TypeLoan>) await _typeLoanRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }
    }
}