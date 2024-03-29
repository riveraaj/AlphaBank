using Dtos.AlphaBank.BankAccounts;

namespace WebClient.Models {
    public class AccountOpeningViewModel {

        public ShowCustomerDto ShowCustomerDto { get; set; } = null!;

        public CreateAccountDto CreateAccountDto { get; set; } = null!;

        public List<ShowAccountForPersonDto> ShowAccountForPersonDtoList { get; set; } = null!;
    }
}