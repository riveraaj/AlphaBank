namespace Dtos.AlphaBank.ContinueLoans {
    public class ShowLoanRecoveringDTO{
        public string LoanId { get; set; } = null!;
        public string PersonId { get; set; } = null!;
        public string LoanStatement { get; set; } = null!;
        public string LoanAmount { get; set; } = null!;
        public string TypeCurrency { get; set; } = null!;
    }
}