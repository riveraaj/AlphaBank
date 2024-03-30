using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public class TypeLoanRepository (AlphaBankDbContext context) : ITypeLoanRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<TypeLoan>> GetAllAsync()
            => await _context.TypeLoans.ToListAsync();

    }
}
