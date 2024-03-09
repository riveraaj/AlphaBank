using Database.AlphaBank;

namespace Service.Security;
public class UserService(AlphaBankDbContext context)  {

    private readonly AlphaBankDbContext _context = context;

    public async Task<bool> ValidateUserId(int id) { 

        var user = await _context.Users.FindAsync(id);

        if(user == null) return false;

        return (id == user.Employee.PersonId);
    }

    public async Task<bool> ValidateUserPassword(int id, string password) {

        var user = await _context.Users.FindAsync(id);

        if(user == null) return false;

        return (user.Password.Equals(password));
    }
}