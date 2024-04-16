using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class TypeLoanRepository(AlphaBankDbContext context) 
                                    : ITypeLoanRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<TypeLoan?> GetById(int id)
            => await _context.TypeLoans.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(TypeLoan oTypeLoan)
            => await _context.TypeLoans.AddAsync(oTypeLoan);

        public async Task<ICollection<TypeLoan>> GetAllAsync()
            => await _context.TypeLoans.ToListAsync();

        public async Task UpdateAsync(TypeLoan oTypeLoan) {
            try {
                var typeLoan = await _context.TypeLoans.FirstOrDefaultAsync(x => x.Id == oTypeLoan.Id)
                    ?? throw new InvalidOperationException("Deadline not found."); ;

                typeLoan.Description = oTypeLoan.Description;
            } catch (Exception e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var typeLoan = await _context.TypeLoans.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new InvalidOperationException("Deadline not found.");

                _context.TypeLoans.Remove(typeLoan);
            } catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}