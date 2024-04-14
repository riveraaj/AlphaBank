using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Mapper.AnalyzeLoanOpportunities
{
    public static class DeadlineMapper
    {
        public static Deadline MapDeadline(CreateDeadlineDTO oCreateDeadlineDTO)
            => new()
            {
                Description = oCreateDeadlineDTO.Description
            };

        public static Deadline MapUpdateDeadline(UpdateDeadlineDTO oUpdateDeadlineDTO)
            => new()
            {
                Id = oUpdateDeadlineDTO.Id,
                Description = oUpdateDeadlineDTO.Description
            };

    }
}
