using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.BankAccounts
{
    public class CreateAccountDto
    {
        [Required]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "El campo ocupación es obligatorio.")]
        public byte? TypeAccountId { get; set; }

        [Required(ErrorMessage = "El campo ocupación es obligatorio.")]
        public byte? TypeCurrencyId { get; set; }

    }
}
