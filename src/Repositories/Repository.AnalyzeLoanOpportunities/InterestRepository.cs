using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class InterestRepository(AlphaBankDbContext context)
                                    : IInterestRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Interest>> GetAllAsync()
            => await _context.Interests.ToListAsync();
    }
}