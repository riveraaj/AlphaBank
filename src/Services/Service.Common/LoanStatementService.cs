using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;

namespace Service.Common
{
    public class LoanStatementService (ILoanStatementRepository loanStatementRepository)
                                        : ILoanStatementService
    {
        private readonly ILoanStatementRepository _loanStatementRepository
            = loanStatementRepository;

        public async Task<List<LoanStatement>> GetAll()
        {
            try
            {
                //Retrieve all LoanStatement asynchronously from the LoanStatementRepository.
                return (List<LoanStatement>) await _loanStatementRepository.GetAllAsync();
            }
            catch
            {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }
    }
}
