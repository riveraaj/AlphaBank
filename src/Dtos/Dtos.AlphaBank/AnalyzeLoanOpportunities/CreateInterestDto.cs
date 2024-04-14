using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.AnalyzeLoanOpportunities
{
    public class CreateInterestDTO
    {
        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [MaxLength(25, ErrorMessage = "El campo descripción tiene que ser menor a 25 carácteres")]
        [Range(1, 99, ErrorMessage = "El campo descripción debe ser un número entero mayor a 0 y menor que 100")]
        public string Description { get; set; } = null!;
    }
}
