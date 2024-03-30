using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.BankAccounts {
    public class CreateAccountDTO {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El campo tipo de cuenta es obligatorio.")]
        public byte? TypeAccountId { get; set; }

        [Required(ErrorMessage = "El campo tipo de moneda es obligatorio.")]
        public byte? TypeCurrencyId { get; set; }
    }
}