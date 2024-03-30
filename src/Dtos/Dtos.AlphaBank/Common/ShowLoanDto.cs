namespace Dtos.AlphaBank.Common
{
    public class ShowLoanDTO
    {
        public int Id { get; set; }
        public int RemainingQuotas { get; set; }
        public int LoanApplicationId { get; set; }
        public string LoanStatementDescription { get; set; } = null!;


    }
}
