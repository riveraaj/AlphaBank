namespace Dtos.AlphaBank.ContinueLoans
{
    public class ShowLoanDefaultersDTO
    {
        public int LoanId { get; set; }
        public string TypeLoanDescription { get; set; } = null!;
        public DateOnly DateApplication { get; set; }
        public int PersonId { get; set; }
        public string Amount { get; set; } = null!;
        public string DaysLate { get; set; } = null!;
    }
}
