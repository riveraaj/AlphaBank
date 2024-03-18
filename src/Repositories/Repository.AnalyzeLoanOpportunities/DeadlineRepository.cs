using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class DeadlineRepository(AlphaBankDbContext context)
                                    : IDeadlineRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Deadline>> GetAllAsync()
            => await _context.Deadlines.ToListAsync();
    }
}