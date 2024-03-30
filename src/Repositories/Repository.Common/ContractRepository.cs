using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class ContractRepository (AlphaBankDbContext context) : IContractRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<Contract>> GetAllAsync()
            => await _context.Contracts.Include(x => x.TypeContract)
                                .ToListAsync();

        public async Task CreateAsync(Contract oContract)
            => await _context.Contracts.AddAsync(oContract);

        public async Task<ICollection<Contract>> GetByLoanApplicationId(int id)
            => await _context.Contracts.Include(x => x.TypeContract)
                                        .Where(x => x.LoanApplicationId == id).ToListAsync();

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}