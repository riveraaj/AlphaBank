using Data.AlphaBank;

namespace Interfaces.Security;

public interface IUserRepository {

    public Task<ICollection<User>> GetAll();

    public Task<User> GetById();

    public Task Create(User oUser);

    public Task Update(User oUser);

    public Task Delete(int id);
}