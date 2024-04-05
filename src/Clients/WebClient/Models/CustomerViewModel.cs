using Dtos.AlphaBank.BankAccounts;

namespace WebClient.Models {
    public class CustomerViewModel {
        public ShowCustomerDTO Customer { get; set; } = null!;
        public CreateCustomerDTO CreateCustomer { get; set; } = null!;
    }
}