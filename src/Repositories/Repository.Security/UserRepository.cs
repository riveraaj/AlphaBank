using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security {
    public class UserRepository(AlphaBankDbContext context) : IUserRepository  {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<User>> GetAllAsync()
            => await _context.Users.Include(x => x.Role)
                                   .Include(x => x.Employee)
                                      .ThenInclude(x => x.Person)       
                                        .ThenInclude(x => x.Phones)
                                   .ToListAsync();

        public async Task<User> GetByIdAsync(int id)
            => await _context.Users.SingleAsync(x => x.Id == id);

        public async Task<User?> GetByPersonIdAsync(int id)
            => await _context.Users
                              .Where(x => x.Employee.PersonId == id)
                              .FirstOrDefaultAsync();

        public async Task CreateAsync(User oUser)
            => await _context.Users.AddAsync(oUser);

        public async Task UpdateAsync(User oUser) {
            try {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == oUser.Id) 
                    ?? throw new InvalidOperationException("User not found.");

                user.EmailAddress = oUser.EmailAddress;
                //user.Password = oUser.Password;
                user.RoleId = oUser.RoleId;
            } catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id) 
                    ?? throw new InvalidOperationException("User not found.");

                _context.Users.Remove(user);
            } catch (SqlException e){
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}