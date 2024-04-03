using Data.AlphaBank;
using Database.AlphaBank;
using Microsoft.EntityFrameworkCore;
using Repository.ContinueLoans.Repositories;

namespace Repository.ContinueLoans {
    public class CollectionHistoryRepository(AlphaBankDbContext context)
                                            : ICollectionHistoryRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<CollectionHistory>> GetAllAsync()
            => await _context.CollectionHistories.ToListAsync();

        public async Task CreateAsync(CollectionHistory oCollectionHistory)
            => await _context.CollectionHistories.AddAsync(oCollectionHistory);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}