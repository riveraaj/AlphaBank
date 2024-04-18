namespace Dtos.AlphaBank.AnalyzeLoanOpportunities
{
    public class ShowLoanApplicationReviewedDTO
    {
        public int Id { get; set; }
        public string TypeLoanDescription { get; set; } = null!;
        public DateOnly DateApplication { get; set; }
        public int PersonId { get; set; }
        public string Amount { get; set; } = null!;
    }
}
