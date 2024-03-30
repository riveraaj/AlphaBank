using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Reports;

namespace Mapper.AnalyzeLoanOpportunities {
    public static class LoanApplicationMapper {

        public static LoanApplication MapLoanApplication(CreateLoanApplicationDTO oCreateLoanApplicationDTO)
            => new() {
                AccountId = oCreateLoanApplicationDTO.AccountId,
                Amount = (decimal) oCreateLoanApplicationDTO.Amount!,
                DeadlineId = (byte) oCreateLoanApplicationDTO.DeadlineId!,
                InterestId = (byte) oCreateLoanApplicationDTO.InterestId!,
                Justification = oCreateLoanApplicationDTO.Justification,
                TypeCurrencyId = (byte) oCreateLoanApplicationDTO.TypeCurrencyId!,
                TypeLoanId = (byte) oCreateLoanApplicationDTO.TypeLoanId!,
                UserId = (int) oCreateLoanApplicationDTO.UserId!
            };

        public static ShowLoanApprovalHistoryDTO MapShowLoanApprovalHistoryDTO
            (LoanApplication oLoanApplication) => new() {
                Amount = oLoanApplication.Amount,
                DateApplication = oLoanApplication.DateApplication,
                Id = oLoanApplication.Id,
                PersonId = oLoanApplication.Account.Customer.PersonId,
                TypeLoan = oLoanApplication.TypeLoan.Description
            };
    }
}