using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.BankAccounts
{
    public class CreateTypeAccountDTO
    {
        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo descripción tiene que ser menor a 50 carácteres")]
        public string Description { get; set; } = null!;
    }
}
