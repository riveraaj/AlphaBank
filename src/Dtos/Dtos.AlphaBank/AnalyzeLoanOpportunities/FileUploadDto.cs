using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.AnalyzeLoanOpportunities {
    public class FileUploadDTO {
        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public string ContentType { get; set; } = null!;

        [Required]
        public byte[] FileContent { get; set; } = null!;
    }
}