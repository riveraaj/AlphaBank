using Database.AlphaBank;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace UnitOfWork {
    public class UnitOfWork (AlphaBankDbContext context) {

        private readonly AlphaBankDbContext _context = context;
        private IDbContextTransaction? _transaction;

        //Init Transaction
        public async void BeginTransaction()
            => _transaction = await _context.Database.BeginTransactionAsync();

        //Confirm Transaction
        public async void CommitTransaction() {
            try {
               await _transaction?.CommitAsync()!;
            }
            catch(SqlException){
                // Handling of exceptions when committing the transaction
                Rollback();
                throw;
            }
            finally {
                _transaction?.Dispose();
                _transaction = null;
            }
        }


        // Revertir Transacción
        public void Rollback() {
            try {
                _transaction?.Rollback();
            }
            finally {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
    }
}