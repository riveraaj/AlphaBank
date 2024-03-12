using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class CreateRoleDto {

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        public string Description { get; set; } = null!;
    }
}