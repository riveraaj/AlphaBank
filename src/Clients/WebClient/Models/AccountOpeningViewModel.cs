using Dtos.AlphaBank.BankAccounts;

namespace WebClient.Models {
    public class AccountOpeningViewModel {

        public ShowCustomerDTO Customer { get; set; } = null!;

        public CreateAccountDTO Account { get; set; } = null!;

        public List<ShowAccountForPersonDTO> AccountList { get; set; } = null!;
    }
}