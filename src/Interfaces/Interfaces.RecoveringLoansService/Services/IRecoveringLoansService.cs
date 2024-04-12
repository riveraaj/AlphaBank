using Dtos.AlphaBank.Common;

namespace Interfaces.RecoveringLoansService.Services {
    public interface IRecoveringLoansService {
        public  Task<bool> JudicialLoanProcess(int loanId, decimal amount);
        public Task<bool> GenerateNewContract(CreateRenegotiationContractDTO oCreateRenegotiationContractDTO);
    }
}