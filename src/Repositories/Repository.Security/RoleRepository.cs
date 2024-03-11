using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security {
    public class RoleRepository(AlphaBankDbContext context) : IRoleRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Role oRole)
            => await _context.Roles.AddAsync(oRole);

        public async Task<ICollection<Role>> GetAllAsync()
            => await _context.Roles.ToListAsync();
    }
}