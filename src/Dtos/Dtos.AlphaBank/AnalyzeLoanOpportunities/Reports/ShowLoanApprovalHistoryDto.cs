namespace Dtos.AlphaBank.Reports {
    public class ShowLoanApprovalHistoryDto {

        public int Id { get; set; }

        public string TypeLoan { get; set; } = null!;

        public DateOnly DateApplication { get; set; }

        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}