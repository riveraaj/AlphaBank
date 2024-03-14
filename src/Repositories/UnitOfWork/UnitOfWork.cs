using Database.AlphaBank;
using Interfaces.Security;
using Microsoft.EntityFrameworkCore.Storage;

namespace UnitOfWork {
    public class UnitOfWork (AlphaBankDbContext context) : IUnitOfWork {

        private readonly AlphaBankDbContext _context = context;
        private IDbContextTransaction? _transaction;

        //Init Transaction
        public async Task BeginTransaction()
            => _transaction = await _context.Database.BeginTransactionAsync();

        //Confirm Transaction
        public async Task CommitTransaction() {
            try {
                if (_transaction != null) {
                    await _transaction.CommitAsync();
                    await _context.SaveChangesAsync();
                }             
            } catch (Exception) {
                // Handling of exceptions when committing the transaction
                if (_transaction != null)
                    await _transaction.RollbackAsync();
                throw;
            } finally {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
    }
}