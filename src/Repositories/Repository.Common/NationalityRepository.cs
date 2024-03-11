using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class NationalityRepository(AlphaBankDbContext context) : INationalityRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Nationality>> GetAllAsync()
            => await _context.Nationalities.ToListAsync();
    }
}