using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Mapper.AnalyzeLoanOpportunities
{
    public static class TypeLoanMapper
    {
        public static TypeLoan MapTypeLoan(CreateTypeLoanDTO oCreateTypeLoaneDTO)
            => new()
            {
                Description = oCreateTypeLoaneDTO.Description
            };

        public static TypeLoan MapUpdateTypeLoan(UpdateTypeLoanDTO oUpdateTypeLoanDTO)
            => new()
            {
                Id = oUpdateTypeLoanDTO.Id,
                Description = oUpdateTypeLoanDTO.Description
            };


    }
}
