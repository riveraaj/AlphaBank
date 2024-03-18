using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public class TypePhoneRepository(AlphaBankDbContext context) : ITypePhoneRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<TypePhone>> GetAllAsync()
            => await _context.TypePhones.ToListAsync();
    }
}