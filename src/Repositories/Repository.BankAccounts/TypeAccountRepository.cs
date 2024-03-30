
using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts
{
    public class TypeAccountRepository (AlphaBankDbContext context) : ITypeAccountRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<TypeAccount>> GetAllAsync()
            => await _context.TypeAccounts.ToListAsync();

    }
}
