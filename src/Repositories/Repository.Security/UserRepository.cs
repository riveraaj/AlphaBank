using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security;

public class UserRepository(AlphaBankDbContext context) : IUserRepository {

    private readonly AlphaBankDbContext _context = context;

    public async Task<ICollection<User>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task<User> GetByIdAsync(int id)
        => await _context.Users.SingleAsync(x => x.Id == id);

    public async Task<User?> GetByPersonIdAsync(int id) 
        => await _context.Users
                          .Where(x => x.Employee.PersonId == id)
                          .FirstOrDefaultAsync();

    public async Task CreateAsync (User oUser) 
        => await _context.Users.AddAsync(oUser);

    public async Task UpdateAsync(int id, User oUser) {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return;

        user.EmailAddress = oUser.EmailAddress;
        user.Password = oUser.Password;
        user.RoleId = oUser.RoleId;
    }

    public async Task RemoveAsync(int id) {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return;

        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == user.EmployeeId);

        if (employee == null)
            return;

        employee.Status = false;
    }
}