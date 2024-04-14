using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security
{
    public class UpdateRoleDTO
    {
        public byte Id { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo descripción tiene que ser menor a 50 carácteres")]
        public string Description { get; set; } = null!;
    }
}
