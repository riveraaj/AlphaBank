using Dtos.AlphaBank.Common;
using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class CreateEmployeeDto {

        [Required(ErrorMessage = "El campo fecha de entrada es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha de entrada debe ser una fecha válida.")]
        public DateOnly? DateEntry { get; set; }

        [Required(ErrorMessage = "El campo categoría de salario es obligatorio.")]
        public byte? SalaryCategoryId { get; set; }

        [Required]
        public required CreatePersonDto Person { get; set; }

        [Required]
        public required CreatePhoneDto Phone { get; set; }

        [Required]
        public required CreateUserDto User { get; set; }
    }
}