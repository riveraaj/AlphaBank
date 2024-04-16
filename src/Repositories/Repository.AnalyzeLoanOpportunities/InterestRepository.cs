using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class InterestRepository(AlphaBankDbContext context)
                                    : IInterestRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Interest?> GetById(int id)
           => await _context.Interests.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Interest oInterest)
            => await _context.Interests.AddAsync(oInterest);

        public async Task<ICollection<Interest>> GetAllAsync()
            => await _context.Interests.ToListAsync();

        public async Task UpdateAsync(Interest oInterest) {
            try {
                var interest = await _context.Interests.FirstOrDefaultAsync(x => x.Id == oInterest.Id)
                    ?? throw new InvalidOperationException("Deadline not found."); ;

                interest.Description = oInterest.Description;
            } catch (Exception e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var interest = await _context.Interests.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new InvalidOperationException("Deadline not found.");

                _context.Interests.Remove(interest!);
            } catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}