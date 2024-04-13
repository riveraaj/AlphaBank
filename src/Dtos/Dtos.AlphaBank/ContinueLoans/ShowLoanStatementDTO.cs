namespace Dtos.AlphaBank.ContinueLoans {
    public  class ShowLoanStatementDTO {
        public string PersonId { get; set; } = null!;
        public string LoanStatement { get; set; } = null!;
        public string CustomerStatus { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}