using Data.AlphaBank;

namespace Interfaces.Security;

public interface IUserRepository {

    public ICollection<User> GetAll();

    public User GetById();

    public void Create(User oUser);

    public void Update(User oUser);

    public void Delete(int id);
}