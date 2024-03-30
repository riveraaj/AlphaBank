using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common
{
    public static class LoanMapper
    {

        public static ShowLoanDTO MapShowLoanDTO(Loan oLoan)
            => new()
            {
                Id = oLoan.Id,
                RemainingQuotas = oLoan.RemainingQuotas,
                LoanApplicationId = oLoan.LoanApplicationId,
                LoanStatementDescription = oLoan.LoanStatement.Description
            };

    }
}
