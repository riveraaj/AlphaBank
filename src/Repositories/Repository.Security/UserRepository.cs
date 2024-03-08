using Database.AlphaBank;

namespace Repository.Security;

public class UserRepository(AlphaBankDbContext context) {

    private readonly AlphaBankDbContext _context = context;

    

}