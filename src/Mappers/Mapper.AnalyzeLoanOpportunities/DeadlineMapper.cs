using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Mapper.AnalyzeLoanOpportunities {
    public static class DeadlineMapper {

        public static ShowCatalogsDTO MapShowDeadlineDTO(Deadline oDeadline)
            => new() {
                Id = oDeadline.Id.ToString(),
                Description = oDeadline.Description
            };

        public static Deadline MapDeadline(CreateDeadlineDTO oCreateDeadlineDTO)
            => new() {
                Description = oCreateDeadlineDTO.Description
            };

        public static Deadline MapUpdateDeadline(UpdateDeadlineDTO oUpdateDeadlineDTO)
            => new() {
                Id = oUpdateDeadlineDTO.Id,
                Description = oUpdateDeadlineDTO.Description
            };

        public static UpdateDeadlineDTO MapUpdateDeadline(Deadline oDeadline)
           => new()  {
               Id = oDeadline.Id,
               Description = oDeadline.Description
           };
    }
}