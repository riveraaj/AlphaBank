using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts
{
    public static class TypeAccountMapper
    {

        public static TypeAccount MapTypeAccount(CreateTypeAccountDTO oCreateTypeAccountDTO)
            => new()
            {
                Description = oCreateTypeAccountDTO.Description
            };

        public static TypeAccount MapUpdateTypeAccount(UpdateTypeAccountDTO oUpdateTypeAccountDTO)
            => new()
            {
                Id = oUpdateTypeAccountDTO.Id,
                Description = oUpdateTypeAccountDTO.Description
            };
    }
}
