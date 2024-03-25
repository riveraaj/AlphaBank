using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public class TypeContractRepository(AlphaBankDbContext context) : ITypeContractRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<TypeContract>> GetAllAsync()
            => await _context.TypeContracts.ToListAsync();

    }
}
