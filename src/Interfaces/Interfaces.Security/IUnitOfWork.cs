namespace Interfaces.Security {
    public interface IUnitOfWork {
        public void BeginTransaction();
        public void CommitTransaction();
        public void Rollback();
    }
}