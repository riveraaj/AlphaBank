using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class UserAuthenticationDTO {

        [Required(ErrorMessage = "El campo id es obligatorio.")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "El campo rol es obligatorio.")]
        public string Role { get; set; } = null!;
    }
}