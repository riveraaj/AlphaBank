
using Dtos.AlphaBank.Common;
using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.BankAccounts {
    public class CreateCustomerDto {

        [Required(ErrorMessage = "El campo correo es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
            ErrorMessage = "El formato del correo no es válido.")]
        [MaxLength(200, ErrorMessage = "El campo correo tiene que ser menor a 200 caracteres")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "El campo salario promedio mensual es obligatorio.")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "El campo salario promedio mensual debe ser un número válido.")]
        public decimal? AverageMonthlySalary { get; set; }

        [Required(ErrorMessage = "El campo ocupación es obligatorio.")]
        public byte? OccupationId { get; set; }

        [Required]
        public required CreatePersonDto Person { get; set; }

        [Required]
        public required CreatePhoneDto Phone { get; set; }
    }
}