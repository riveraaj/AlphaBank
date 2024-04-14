using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.AnalyzeLoanOpportunities
{
    public class UpdateTypeLoanDTO
    {
        public byte Id { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [MaxLength(25, ErrorMessage = "El campo descripción tiene que ser menor a 25 carácteres")]
        public string Description { get; set; } = null!;
    }
}
