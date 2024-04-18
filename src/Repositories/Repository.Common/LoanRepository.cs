using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public class LoanRepository (AlphaBankDbContext context) : ILoanRepository
    {

        private readonly AlphaBankDbContext _context = context;

        public async Task<Loan?> GetById(int id)
            => await _context.Loans.Include(ls => ls.LoanStatement)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.TypeCurrency)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Interest)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Deadline)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Account)
                                            .ThenInclude(c => c.Customer)
                                                .ThenInclude(p => p.Person)
                                                    .ThenInclude(ti => ti.TypeIdentification)
                                    .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Loan?> GetByLoanApplicationId(int id)
            => await _context.Loans.Include(ls => ls.LoanStatement)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.TypeCurrency)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Interest)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Deadline)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Account)
                                            .ThenInclude(c => c.Customer)
                                                .ThenInclude(p => p.Person)
                                                    .ThenInclude(ti => ti.TypeIdentification)
                                    .FirstOrDefaultAsync(x => x.LoanApplicationId == id);

        public async Task<ICollection<Loan>> GetByPersonId(int id)
            => await _context.Loans.Include(ls => ls.LoanStatement)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.TypeCurrency)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Interest)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Deadline)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Account)
                                            .ThenInclude(c => c.Customer)
                                                .ThenInclude(p => p.Person)
                                                    .ThenInclude(ti => ti.TypeIdentification)
                                    .Where(x => x.LoanApplication.Account.Customer.PersonId == id).ToListAsync();

        public async Task<ICollection<Loan>> GetAllAsync()
            => await _context.Loans.Include(ls => ls.LoanStatement)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.TypeCurrency)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.TypeLoan)
                                   .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Interest)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Deadline)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Account)
                                            .ThenInclude(c => c.Customer)
                                                .ThenInclude(p => p.Person)
                                                    .ThenInclude(ti => ti.TypeIdentification)
                                    .Include(la => la.LoanApplication)
                                        .ThenInclude(tc => tc.Account)
                                            .ThenInclude(c => c.Customer)
                                                .ThenInclude(p => p.CustomerStatus)
                                    .Include(la => la.LoanApplication)
                                         .ThenInclude(tc => tc.Account)
                                             .ThenInclude(c => c.Customer)
                                                 .ThenInclude(p => p.Person)
                                                     .ThenInclude(ti => ti.Phones)
                                    .ToListAsync();

        public async Task CreateAsync(Loan oLoan)
            => await _context.Loans.AddAsync(oLoan);

        public async Task UpdateLoanStatementAsync(int id, byte loanStatementId) {
            var loan = await _context.Loans.FirstOrDefaultAsync(x => x.Id == id);
            loan!.LoanStatementId = loanStatementId;
        }

        public async Task UpdateRemainingQuotasAsync(int id) {
            var loan = await _context.Loans.FirstOrDefaultAsync(x => x.Id == id);
            loan!.RemainingQuotas -= 1;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}