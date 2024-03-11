namespace Interfaces.Security {
    public interface IUnitOfWork {
        public Task BeginTransaction();
        public Task CommitTransaction();
        public Task RollbackAsync();

        public Task SaveChangesAsync();
    }
}