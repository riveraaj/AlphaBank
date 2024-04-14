using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.AnalyzeLoanOpportunities
{
    public class UpdateDeadlineDTO
    {
        public byte Id { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [MaxLength(25, ErrorMessage = "El campo descripción tiene que ser menor a 25 carácteres")]
        [RegularExpression(@"^(?!0 Meses$)\d+ Meses$",
            ErrorMessage = "El campo descripción debe tener el formato '# Meses', por ejemplo '128 Meses' y no puede ser '0 Meses'")]
        public string Description { get; set; } = null!;

    }
}
