using Data.AlphaBank;
using Database.AlphaBank;
using Microsoft.EntityFrameworkCore;
using Interfaces.ContinueLoans.Repositories;

namespace Repository.ContinueLoans {
    public class CollectionHistoryRepository(AlphaBankDbContext context)
                                            : ICollectionHistoryRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<CollectionHistory>> GetAllAsync()
            => await _context.CollectionHistories.Include(x => x.Loan)
                                                    .ThenInclude(x => x.LoanApplication)
                                                        .ThenInclude(x => x.Account)
                                                            .ThenInclude(x => x.Customer)
                                                                .ThenInclude(x => x.Person)
                                                 .Include(x => x.Loan)
                                                    .ThenInclude(x => x.LoanApplication)
                                                        .ThenInclude(x => x.TypeCurrency)
                                                 .ToListAsync();

        public async Task<CollectionHistory?> GetLastByLoanId(int id)
            => await _context.CollectionHistories.Include(x => x.Loan)  
                                                    .ThenInclude(x => x.LoanApplication)
                                                        .ThenInclude(x => x.Account)
                                                            .ThenInclude(x => x.Customer)
                                                                .ThenInclude(x => x.Person)
                                                  .Include(x => x.Loan)
                                                    .ThenInclude(x => x.LoanApplication)
                                                        .ThenInclude(x => x.TypeCurrency) 
                                                  .Where(x => x.LoanId == id)
                                                  .OrderByDescending(x => x.DateDeposit)
                                                  .FirstOrDefaultAsync(x => x.LoanId == id);

        public async Task<List<CollectionHistory>> GetByLoanId(int id)
            => await _context.CollectionHistories.Where(x => x.LoanId == id).ToListAsync();

        public async Task CreateAsync(CollectionHistory oCollectionHistory)
            => await _context.CollectionHistories.AddAsync(oCollectionHistory);

        public async Task UpdateAsync(int id, decimal mount){
            try {
                var collection = await _context.CollectionHistories.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new NullReferenceException("Collection history not found");

                collection.MoratoriumInterest = 0;
                collection.DepositMount = mount;
            }
            catch (Exception) {
                throw;
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}