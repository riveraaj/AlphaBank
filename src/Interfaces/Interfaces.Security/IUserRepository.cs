using Data.AlphaBank;

namespace Interfaces.Security;

public interface IUserRepository {

    public Task<ICollection<User>> GetAllAsync();

    public Task<User> GetByIdAsync(int id);

    public Task<User?> GetByOPersonIdAsync(int id);

    public Task CreateAsync(User oUser);

    public Task UpdateAsync(int id, User oUser);

    public Task RemoveAsync(int id);
}