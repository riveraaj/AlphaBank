using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Common
{
    public class UpdateTypeCurrencyDTO
    {
        public byte Id { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [MaxLength(3, ErrorMessage = "El campo descripción tiene que ser menor a 3 carácteres")]
        [RegularExpression("^[A-Z]+$", ErrorMessage = "El campo descripción debe contener solo letras mayúsculas.")]
        public string Description { get; set; } = null!;
    }
}
