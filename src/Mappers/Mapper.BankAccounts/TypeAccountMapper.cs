using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts {
    public static class TypeAccountMapper {

        public static ShowCatalogsDTO MapShowTypeAccountDTO(TypeAccount oTypeAccount)
          => new() {
              Id = oTypeAccount.Id.ToString(),
              Description = oTypeAccount.Description
          };

        public static TypeAccount MapTypeAccount(CreateTypeAccountDTO oCreateTypeAccountDTO)
            => new() {
                Description = oCreateTypeAccountDTO.Description
            };

        public static TypeAccount MapUpdateTypeAccount(UpdateTypeAccountDTO oUpdateTypeAccountDTO)
            => new() {
                Id = oUpdateTypeAccountDTO.Id,
                Description = oUpdateTypeAccountDTO.Description
            };

        public static UpdateTypeAccountDTO MapUpdateTypeAccount(TypeAccount oTypeAccount)
           => new() {
               Id = oTypeAccount.Id,
               Description = oTypeAccount.Description
           };
    }
}