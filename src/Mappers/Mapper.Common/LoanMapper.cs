using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.ContinueLoans;

namespace Mapper.Common {
    public static class LoanMapper {

        public static ShowLoanDTO MapShowLoanDTO(Loan oLoan)
            => new()
            {
                Id = oLoan.Id,
                RemainingQuotas = oLoan.RemainingQuotas,
                LoanApplicationId = oLoan.LoanApplicationId,
                LoanStatementDescription = oLoan.LoanStatement.Description
            };

        public static ShowLoanStatementDTO MapShowLoanStatementDTO(Loan oLoan)
            => new() {
                CustomerStatus = oLoan.LoanApplication.Account.Customer.CustomerStatus.Description,
                LoanStatement = oLoan.LoanStatement.Description,
                LoanId = oLoan.Id.ToString(),
                FullName = $"{oLoan.LoanApplication.Account.Customer.Person.Name} " +
                $"{oLoan.LoanApplication.Account.Customer.Person.FirstName} " +
                $"{oLoan.LoanApplication.Account.Customer.Person.SecondName}",
                PersonId = oLoan.LoanApplication.Account.Customer.PersonId.ToString(),
                PhoneNumber = oLoan.LoanApplication.Account.Customer.Person.Phones.FirstOrDefault()!.Number.ToString()
            };
    }
}