using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class CreateRoleDTO {

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        public string Description { get; set; } = null!;
    }
}