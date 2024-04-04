namespace Dtos.AlphaBank.AnalyzeLoanOpportunities {
    public class ShowLoanApplicationDTO {
        public int Id { get; set; }
        public string TypeLoan { get; set; } = null!;
        public DateOnly DateApplication { get; set; }
        public int PersonId { get; set; }
        public string Amount { get; set; } = null!;
        public string TypeCurrency { get; set; } = null!;
    }
}