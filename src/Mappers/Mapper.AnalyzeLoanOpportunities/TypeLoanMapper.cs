using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Mapper.AnalyzeLoanOpportunities {
    public static class TypeLoanMapper {

        public static ShowCatalogsDTO MapShowTypeLoanDTO(TypeLoan oTypeLoan)
           => new() {
               Id = oTypeLoan.Id.ToString(),
               Description = oTypeLoan.Description
           };

        public static TypeLoan MapTypeLoan(CreateTypeLoanDTO oCreateTypeLoaneDTO)
            => new() {
                Description = oCreateTypeLoaneDTO.Description
            };

        public static TypeLoan MapUpdateTypeLoan(UpdateTypeLoanDTO oUpdateTypeLoanDTO)
            => new() {
                Id = oUpdateTypeLoanDTO.Id,
                Description = oUpdateTypeLoanDTO.Description
            };

        public static UpdateTypeLoanDTO MapUpdateTypeLoan(TypeLoan oTypeLoan)
           => new() {
               Id = oTypeLoan.Id,
               Description = oTypeLoan.Description
           };
    }
}