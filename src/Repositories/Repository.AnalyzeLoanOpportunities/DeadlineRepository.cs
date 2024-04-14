using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class DeadlineRepository(AlphaBankDbContext context)
                                    : IDeadlineRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Deadline oDeadline)
            => await _context.Deadlines.AddAsync(oDeadline);

        public async Task<ICollection<Deadline>> GetAllAsync()
            => await _context.Deadlines.ToListAsync();

        public async Task UpdateAsync(Deadline oDeadline)
        {
            var deadline = await _context.Deadlines.FirstOrDefaultAsync(x => x.Id == oDeadline.Id);

            if (deadline == null) return;

            deadline.Description = oDeadline.Description;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var deadline = await _context.Deadlines.FirstOrDefaultAsync(x => x.Id == id);

            if (deadline != null) _context.Deadlines.Remove(deadline);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}