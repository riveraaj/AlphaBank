using Data.AlphaBank;
using Database.AlphaBank;
using Microsoft.EntityFrameworkCore;
using Interfaces.ContinueLoans.Repositories;

namespace Repository.ContinueLoans {
    public class CollectionHistoryRepository(AlphaBankDbContext context)
                                            : ICollectionHistoryRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<CollectionHistory>> GetAllAsync()
            => await _context.CollectionHistories.ToListAsync();

        public async Task<CollectionHistory?> GetLastByLoanId(int id)
            => await _context.CollectionHistories.Include(x => x.Loan)  
                                                    .ThenInclude(x => x.LoanApplication)
                                                        .ThenInclude(x => x.Account)
                                                            .ThenInclude(x => x.Customer)
                                                                .ThenInclude(x => x.Person)
                                                  .Include(x => x.Loan)
                                                    .ThenInclude(x => x.LoanApplication)
                                                        .ThenInclude(x => x.TypeCurrency)                    
                                                  .LastOrDefaultAsync(x => x.LoanId == id);

        public async Task<List<CollectionHistory>> GetByLoanId(int id)
            => await _context.CollectionHistories.Where(x => x.LoanId == id).ToListAsync();

        public async Task CreateAsync(CollectionHistory oCollectionHistory)
            => await _context.CollectionHistories.AddAsync(oCollectionHistory);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}