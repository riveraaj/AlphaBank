using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank {
    public class UserLoginDto {

        [Required(ErrorMessage = "Por favor, ingrese su identificación.")]
        [Display(Name = "identificación")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese su contraseña.")]
        [Display(Name = "contraseña")]
        public string Password { get; set; } = null!;
    }
}