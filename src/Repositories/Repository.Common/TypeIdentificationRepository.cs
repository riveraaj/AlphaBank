using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class TypeIdentificationRepository(AlphaBankDbContext context) : ITypeIdentificationRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<TypeIdentification>> GetAllAsync()
            => await _context.TypeIdentifications.ToListAsync();
    }
}