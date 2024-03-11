using Database.AlphaBank;
using Interfaces.Security;
using Microsoft.EntityFrameworkCore;
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
                if (_transaction != null)
                    await _transaction.CommitAsync();          
            }
            catch (DbUpdateException) {
                // Handling of exceptions when committing the transaction
                await RollbackAsync();
                throw;
            }
            catch (Exception) {
                // Handling of exceptions when committing the transaction
                await RollbackAsync();
                throw;
            }
            finally {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        //Revert Transaction
        public async Task RollbackAsync() {
            try {
                await _transaction?.RollbackAsync()!;
            }
            finally {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task SaveChangesAsync() {
            try  {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {   
                throw;
            }
        }
    }
}