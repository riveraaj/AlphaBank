using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank {
    public class UserLoginDto {

        [Required(ErrorMessage = "Por favor, ingrese su identificación")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor, ingrese una identificación valida.")]
        [Display(Name = "identificación")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese su contraseña.")]
        [Display(Name = "contraseña")]
        public string Password { get; set; } = null!;
    }
}