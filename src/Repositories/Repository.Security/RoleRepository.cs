using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security {
    public class RoleRepository(AlphaBankDbContext context) : IRoleRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Role?> GetById(int id)
            => await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Role oRole)
            => await _context.Roles.AddAsync(oRole);

        public async Task<ICollection<Role>> GetAllAsync()
            => await _context.Roles.ToListAsync();

        public async Task UpdateAsync(Role oRole) {
            try {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == oRole.Id)
                   ?? throw new InvalidOperationException("Deadline not found."); ;

                role.Description = oRole.Description;
            } catch (Exception e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new InvalidOperationException("Deadline not found.");

                _context.Roles.Remove(role);
            } catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();     
    }
}