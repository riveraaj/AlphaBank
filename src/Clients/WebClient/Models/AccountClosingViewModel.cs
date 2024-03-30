using Dtos.AlphaBank.BankAccounts;

namespace WebClient.Models {
    public class AccountClosingViewModel {

        public ShowCustomerDTO Customer { get; set; } = null!;
        public List<ShowAccountForPersonDTO> AccountList { get; set; } = null!;
    }
}