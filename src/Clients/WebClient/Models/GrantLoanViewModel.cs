using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace WebClient.Models {
    public class GrantLoanViewModel{
        public ShowLoanApplicationDTO LoanApplication { get; set; } = null!;
        public ShowCustomerLoanDTO Customer { get; set; } = null!;
        public List<ShowLoanApplicationDTO> LoanApplicationList { get; set; } = null!;
    }
}