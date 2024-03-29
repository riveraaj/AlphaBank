using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace WebClient.Models {
    public class LoanApplicationViewModel {

        public ShowCustomerLoanDto ShowCustomerLoanDto { get; set; } = null!;

        public CreateLoanApplicationDto CreateLoanApplicationDto { get; set; } = null!;
    }
}