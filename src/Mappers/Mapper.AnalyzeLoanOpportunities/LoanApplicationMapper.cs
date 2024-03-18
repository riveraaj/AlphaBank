using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

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
    }
}