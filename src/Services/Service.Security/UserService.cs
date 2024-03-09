using Database.AlphaBank;
using Interfaces.Security;

namespace Service.Security;
public class UserService(AlphaBankDbContext context) : IUserService {

    private readonly AlphaBankDbContext _context = context;

    //This function returns true or false if a user with the credential id is found.
    public async Task<bool> ValidateUserId(int id) { 

        var user = await _context.Users.FindAsync(id);

        if(user == null) return false;

        return (id == user.Employee.PersonId);
    }

    //This function returns true or false if the contrasenna credentials are correct.
    public async Task<bool> ValidateUserPassword(int id, string password) {

        var user = await _context.Users.FindAsync(id);

        if(user == null) return false;

        return (user.Password.Equals(password));
    }
}