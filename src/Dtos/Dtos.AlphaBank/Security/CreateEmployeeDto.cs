using Dtos.AlphaBank.Common;
using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class CreateEmployeeDTO {

        [Required(ErrorMessage = "El campo fecha de entrada es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha de entrada debe ser una fecha válida.")]
        public DateOnly? DateEntry { get; set; }

        [Required(ErrorMessage = "El campo categoría de salario es obligatorio.")]
        public byte? SalaryCategoryId { get; set; }

        [Required]
        public required CreatePersonDTO Person { get; set; }

        [Required]
        public required CreatePhoneDTO Phone { get; set; }

        [Required]
        public required CreateUserDTO User { get; set; }
    }
}