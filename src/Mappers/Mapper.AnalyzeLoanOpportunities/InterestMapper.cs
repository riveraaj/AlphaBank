using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Mapper.AnalyzeLoanOpportunities {
    public static class InterestMapper {

        public static ShowCatalogsDTO MapShowInterestDTO(Interest oInterest)
           => new() {
               Id = oInterest.Id.ToString(),
               Description = oInterest.Description
           };

        public static Interest MapInterest(CreateInterestDTO oCreateInterestDTO)
            => new() {
                Description = oCreateInterestDTO.Description
            };

        public static Interest MapUpdateInterest(UpdateInterestDTO oUpdateInterestDTO)
            => new() {
                Id = oUpdateInterestDTO.Id,
                Description = oUpdateInterestDTO.Description
            };

        public static UpdateInterestDTO MapUpdateInterest(Interest oInterest)
            => new()  {
                Id = oInterest.Id,
                Description = oInterest.Description
            };
    }
}