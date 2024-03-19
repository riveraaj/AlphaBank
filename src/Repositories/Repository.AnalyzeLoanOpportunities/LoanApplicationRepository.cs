using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.AnalyzeLoanOpportunities {
    public class LoanApplicationRepository(AlphaBankDbContext context) 
                                 : ILoanApplicationRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<LoanApplication>> GetAllAsync()
            => await _context.LoanApplications.ToListAsync();

        public async Task Create(LoanApplication oLoanApplication)
            => await _context.LoanApplications.AddAsync(oLoanApplication);

        public async Task UpdateApplicationStatus(int id, byte statusId) {
            var aplicationStatus = await _context.LoanApplications.FirstOrDefaultAsync(x => x.Id == id);

            if (aplicationStatus == null)
                return;

            aplicationStatus.ApplicationStatusId = statusId;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}