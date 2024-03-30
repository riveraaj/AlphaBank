using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class MaritalStatusRepository(AlphaBankDbContext context) : IMaritalStatusRepository  {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<MaritalStatus>> GetAllAsync() 
            => await _context.MaritalStatuses.ToListAsync();
    }
}