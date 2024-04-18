using Data.AlphaBank;
using Dtos.AlphaBank.ContinueLoans;
using System.Globalization;

namespace Mapper.ContinueLoans {
    public static class CollectionHistoryMapper {

        public static CollectionHistory MapCollectionHistory(CreateCollectionHistoryDTO oCreateCollectionHistoryDTO)
            => new() {
                DepositMount = (int) oCreateCollectionHistoryDTO.DepositAmount!,
                LoanId = (int) oCreateCollectionHistoryDTO.LoanId!,
            };

        public static ShowLoanDefaultersDTO MapShowLoanDefaultersDTO(Loan oLoan, string daysLate)
            => new()
            {
                LoanId = oLoan.Id,
                TypeLoanDescription = oLoan.LoanApplication.TypeLoan.Description,
                DateApplication = oLoan.LoanApplication.DateApplication,
                PersonId = oLoan.LoanApplication.Account.Customer.PersonId,
                Amount = oLoan.LoanApplication.Amount.ToString("#,0", CultureInfo.InvariantCulture),
                DaysLate = daysLate
            };
    }
}