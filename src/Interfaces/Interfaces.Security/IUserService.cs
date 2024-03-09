namespace Interfaces.Security;
public  interface IUserService {

    public Task<bool> ValidateUserId(int id);
    public Task<bool> ValidateUserPassword(int id, string password);
}