using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class InterestRepository(AlphaBankDbContext context)
                                    : IInterestRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Interest oInterest)
            => await _context.Interests.AddAsync(oInterest);

        public async Task<ICollection<Interest>> GetAllAsync()
            => await _context.Interests.ToListAsync();

        public async Task UpdateAsync(Interest oInterest)
        {
            var interest = await _context.Interests.FirstOrDefaultAsync(x => x.Id == oInterest.Id);

            if (interest == null) return;

            interest.Description = oInterest.Description;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var interest = await _context.Interests.FirstOrDefaultAsync(x => x.Id == id);

            if (interest != null) _context.Interests.Remove(interest);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}