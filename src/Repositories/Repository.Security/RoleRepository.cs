using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security {
    public class RoleRepository(AlphaBankDbContext context) : IRoleRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Role oRole)
            => await _context.Roles.AddAsync(oRole);

        public async Task<ICollection<Role>> GetAllAsync()
            => await _context.Roles.ToListAsync();

        public async Task UpdateAsync(Role oRole)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == oRole.Id);

            if (role == null) return;

            role.Description = oRole.Description;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (role != null) _context.Roles.Remove(role);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();     
    }
}