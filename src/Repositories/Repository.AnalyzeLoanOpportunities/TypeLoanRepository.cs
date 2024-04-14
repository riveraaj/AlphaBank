using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class TypeLoanRepository(AlphaBankDbContext context) 
                                    : ITypeLoanRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(TypeLoan oTypeLoan)
            => await _context.TypeLoans.AddAsync(oTypeLoan);

        public async Task<ICollection<TypeLoan>> GetAllAsync()
            => await _context.TypeLoans.ToListAsync();

        public async Task UpdateAsync(TypeLoan oTypeLoan)
        {
            var typeLoan = await _context.TypeLoans.FirstOrDefaultAsync(x => x.Id == oTypeLoan.Id);

            if (typeLoan == null) return;

            typeLoan.Description = oTypeLoan.Description;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var typeLoan = await _context.TypeLoans.FirstOrDefaultAsync(x => x.Id == id);

            if (typeLoan != null) _context.TypeLoans.Remove(typeLoan);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}