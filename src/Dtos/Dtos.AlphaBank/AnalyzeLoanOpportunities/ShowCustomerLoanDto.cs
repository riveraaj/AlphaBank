namespace Dtos.AlphaBank.AnalyzeLoanOpportunities {
    public class ShowCustomerLoanDTO {
        public string FullName { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public int Age { get; set; }
        public string Nationality { get; set; } = null!;
        public string Occupation { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string AverageMonthlySalary { get; set; } = null!;
    }
}