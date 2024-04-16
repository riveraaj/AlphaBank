using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class DeadlineRepository(AlphaBankDbContext context)
                                    : IDeadlineRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Deadline?> GetById(int id)
            => await _context.Deadlines.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Deadline oDeadline)
            => await _context.Deadlines.AddAsync(oDeadline);

        public async Task<ICollection<Deadline>> GetAllAsync()
            => await _context.Deadlines.ToListAsync();

        public async Task UpdateAsync(Deadline oDeadline) {
            try {
                var deadline = await _context.Deadlines.FirstOrDefaultAsync(x => x.Id == oDeadline.Id)
                    ?? throw new InvalidOperationException("Deadline not found."); ;

                deadline.Description = oDeadline.Description;
            }
            catch (Exception e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var deadline = await _context.Deadlines.FirstOrDefaultAsync(x => x.Id == id) 
                    ?? throw new InvalidOperationException("Deadline not found.");

                _context.Deadlines.Remove(deadline);
            }
            catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}