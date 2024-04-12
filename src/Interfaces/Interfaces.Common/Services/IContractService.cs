using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Interfaces.Common.Services {
    public interface IContractService {
        public Task<bool> LoanTypeCreate(LoanApplication oLoanApplication);
        public Task<bool> RenegotiationTypeCreate(Loan oLoan, string newInterestPercentage, string newLoanTerm, decimal newLoanAmount);
        public Task<List<ShowContractDTO>> GetByLoanApplicationID(int id);
        public Task<Contract?> GetByLoanApplicationId(int id);
    }
}