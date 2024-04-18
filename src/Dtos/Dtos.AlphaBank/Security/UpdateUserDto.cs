using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class UpdateUserDTO {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo correo es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
            ErrorMessage = "El formato del correo no es válido.")]
        [MaxLength(200, ErrorMessage = "El campo correo tiene que ser menor a 200 caracteres")]
        public string Email { get; set; } = null!;

        //[Required(ErrorMessage = "El campo contraseña es obligatorio.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        //    ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula, un número y un mínimo de 8 caracteres.")]
        //public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El campo rol es obligatorio.")]
        [Range(0, 20, ErrorMessage = "El valor de rol debe estar entre 0 y 20.")]
        public byte? RoleId { get; set; }
    }
}