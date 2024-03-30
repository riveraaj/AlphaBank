using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Common {
    public class CreatePhoneDto {

        [Required(ErrorMessage = "El campo tipo de telefono es obligatorio.")]
        public byte? TypePhoneId { get; set; }

        [Required(ErrorMessage = "El campo numero de telefono es obligatorio.")]
        public int? PhoneNumber { get; set; }
        public int PersonId { get; set; }

    }
} 