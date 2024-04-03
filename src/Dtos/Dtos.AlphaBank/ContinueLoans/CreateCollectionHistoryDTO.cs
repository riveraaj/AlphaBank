using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.ContinueLoans {
    public class CreateCollectionHistoryDTO {
        [Required]
        public decimal? DepositAmount { get; set; }
        [Required]
        public int? LoanId { get; set; }
    }
}