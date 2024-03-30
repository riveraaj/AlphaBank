namespace Dtos.AlphaBank.BankAccounts {
    public class ShowCustomerDTO {
        public string CustomerId { get; set; } = null!;
        public int PersonId { get; set; }
        public string FullName { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public int Age { get; set; }
        public string Nationality { get; set; } = null!;
        public string Occupation { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
    }
}