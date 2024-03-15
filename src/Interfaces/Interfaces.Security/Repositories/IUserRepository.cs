using Data.AlphaBank;

namespace Interfaces.Security.Repositories
{

    public interface IUserRepository
    {

        public Task<ICollection<User>> GetAllAsync();

        public Task<User> GetByIdAsync(int id);

        public Task<User?> GetByPersonIdAsync(int id);

        public Task CreateAsync(User oUser);

        public Task UpdateAsync(int id, User oUser);

        public Task RemoveAsync(int id);

        public Task SaveChangesAsync();
    }
}