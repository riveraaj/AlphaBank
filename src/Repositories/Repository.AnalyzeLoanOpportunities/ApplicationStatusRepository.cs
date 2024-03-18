using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class ApplicationStatusRepository(AlphaBankDbContext context)
                                                : IApplicationStatusRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<ApplicationStatus>> GetAllAsync()
            => await _context.ApplicationStatuses.ToListAsync();
    }
}