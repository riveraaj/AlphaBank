using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace WebClient.Models {
    public class LoanApplicationViewModel {

        public ShowCustomerLoanDTO Customer { get; set; } = null!;

        public CreateLoanApplicationDTO LoanApplication { get; set; } = null!;
    }
}