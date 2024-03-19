using Data.AlphaBank;
using Dtos.AlphaBank.Reports;

namespace Mapper.AnalyzeLoanOpportunities.Reports { 
    public static class LoanApplicationMapper {

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