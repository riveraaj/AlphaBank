using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Security {
    public class CreateEmployeeDto {

        [Required]
        public int? PersonId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string SecondName { get; set; } = null!;

        [Required]
        public DateOnly? DateBirth { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public byte? TypeIdentificationId { get; set; }

        [Required]
        public byte? NationalityId { get; set; }

        [Required]
        public byte? MaritalStatusId { get; set; }

        [Required]
        public DateOnly? DateEntry { get; set; }

        [Required]
        public byte? SalaryCategoryId { get; set; }

    }
}