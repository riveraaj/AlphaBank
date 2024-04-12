using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Common {
    public class CreateRenegotiationContractDTO {

        [Required(ErrorMessage = "El campo id es obligatorio")]
        public int LoanId { get; set; }

        [Required(ErrorMessage = "El campo interés es obligatorio")]
        public byte? NewInterestPercentage { get; set; }
        [Required(ErrorMessage = "El campo plazo es obligatorio")]
        public byte? NewLoanTerm { get; set; }
        [Required(ErrorMessage = "El campo nuevo monto es obligatorio")]
        public decimal? NewLoanAmount { get; set; }
    }
}