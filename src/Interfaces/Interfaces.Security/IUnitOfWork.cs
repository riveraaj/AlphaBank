namespace Interfaces.Security {
    public interface IUnitOfWork {
        public Task BeginTransaction();
        public Task CommitTransaction();
    }
}