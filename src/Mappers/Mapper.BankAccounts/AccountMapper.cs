using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts
{
    public static class AccountMapper
    {

        public static Account MapAccount(CreateAccountDto oCreateAccountDto)
            => new()
            {
                CustomerId = (int) oCreateAccountDto.CustomerId!,
                TypeAccountId = (byte) oCreateAccountDto.TypeAccountId!,
                TypeCurrencyId = (byte) oCreateAccountDto.TypeCurrencyId!,
            };

    }
}
