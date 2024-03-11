namespace Dtos.AlphaBank.Security {
    public class CreateEmployeeDto {

        public int? PersonId { get; set; }

        public string Name { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;

        public DateOnly? DateBirth { get; set; }

        public string Address { get; set; } = null!;

        public byte? TypeIdentificationId { get; set; }

        public byte? NationalityId { get; set; }

        public byte? MaritalStatusId { get; set; }

        public DateOnly? DateEntry { get; set; }

        public byte? SalaryCategoryId { get; set; }

    }
}