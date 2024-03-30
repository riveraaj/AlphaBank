using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Reports;

namespace Mapper.AnalyzeLoanOpportunities {
    public static class LoanApplicationMapper {

        public static LoanApplication MapLoanApplication(CreateLoanApplicationDto oCreateLoanApplicationDto)
            => new() {
                AccountId = oCreateLoanApplicationDto.AccountId,
                Amount = (decimal) oCreateLoanApplicationDto.Amount!,
                DeadlineId = (byte) oCreateLoanApplicationDto.DeadlineId!,
                InterestId = (byte) oCreateLoanApplicationDto.InterestId!,
                Justification = oCreateLoanApplicationDto.Justification,
                TypeCurrencyId = (byte) oCreateLoanApplicationDto.TypeCurrencyId!,
                TypeLoanId = (byte) oCreateLoanApplicationDto.TypeLoanId!,
                UserId = (int) oCreateLoanApplicationDto.UserId!
            };

        public static ShowLoanApprovalHistoryDto MapShowLoanApprovalHistoryDto
            (LoanApplication oLoanApplication) => new() {
                Amount = oLoanApplication.Amount,
                DateApplication = oLoanApplication.DateApplication,
                Id = oLoanApplication.Id,
                PersonId = oLoanApplication.Account.Customer.PersonId,
                TypeLoan = oLoanApplication.TypeLoan.Description
            };
    }
}