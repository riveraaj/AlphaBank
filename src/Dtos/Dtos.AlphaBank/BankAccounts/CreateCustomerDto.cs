
using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.BankAccounts
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "El campo identificación es obligatorio.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo identificación debe contener solo números enteros.")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo nombre no puede tener más de 25 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo primer apellido es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo primer apellido no puede tener más de 25 caracteres.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "El campo segundo apellido es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo segundo apellido no puede tener más de 25 caracteres.")]
        public string SecondName { get; set; } = null!;

        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha de nacimiento  debe ser una fecha válida.")]
        public DateOnly? DateBirth { get; set; }

        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        [StringLength(200, ErrorMessage = "El campo dirección no puede tener más de 200 caracteres.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "El campo tipo de identificación es obligatorio.")]
        public byte? TypeIdentificationId { get; set; }

        [Required(ErrorMessage = "El campo nacionalidad es obligatorio.")]
        public byte? NationalityId { get; set; }

        [Required(ErrorMessage = "El campo estado civil es obligatorio.")]
        public byte? MaritalStatusId { get; set; }

        [Required(ErrorMessage = "El campo número de teléfono es obligatorio.")]
        public int? Number { get; set; }

        [Required(ErrorMessage = "El campo tipo de telefono es obligatorio.")]
        public byte? TypePhoneId { get; set; }

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

        [Required(ErrorMessage = "El campo estado del cliente es obligatorio.")]
        public byte? CustomerStatusId { get; set; }

    }
}
