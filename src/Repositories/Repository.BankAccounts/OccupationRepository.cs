using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts {
    public class OccupationRepository(AlphaBankDbContext context) : IOccupationRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Occupation>> GetAllAsync()
          => await _context.Occupations.ToListAsync();
    }
}