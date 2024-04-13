namespace Dtos.AlphaBank.AnalyzeLoanOpportunities {
    public class ShowLoanApplicationDTO {
        public int Id { get; set; }
        public string TypeLoan { get; set; } = null!;
        public DateOnly DateApplication { get; set; }
        public int PersonId { get; set; }
        public string Amount { get; set; } = null!;
        public string TypeCurrency { get; set; } = null!;
        public string Deadline { get; set; } = null!;
        public string Interest {  get; set; } = null!;
        public string EmployerOrder { get; set; } = null!;
        public string SalaryStatement { get; set; } = null!;
    }
}