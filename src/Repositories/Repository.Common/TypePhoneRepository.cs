using Data.AlphaBank;
using Database.AlphaBank;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class TypePhoneRepository(AlphaBankDbContext context) {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<TypePhone>> GetAllAsync()
            => await _context.TypePhones.ToListAsync();

    }
}