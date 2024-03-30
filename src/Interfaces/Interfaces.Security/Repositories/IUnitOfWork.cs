namespace Interfaces.Security.Repositories
{
    public interface IUnitOfWork
    {
        public Task BeginTransaction();
        public Task CommitTransaction();
    }
}