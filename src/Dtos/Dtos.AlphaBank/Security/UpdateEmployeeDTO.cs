using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class UpdateEmployeeDTO {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "El campo tipo de telefono es obligatorio.")]
        public byte? TypePhoneId { get; set; }

        [Required(ErrorMessage = "El campo numero de telefono es obligatorio.")]
        public int? PhoneNumber { get; set; }
        [Required(ErrorMessage = "El campo categoría de salario es obligatorio.")]
        public byte? SalaryCategoryId { get; set; }
    }
}