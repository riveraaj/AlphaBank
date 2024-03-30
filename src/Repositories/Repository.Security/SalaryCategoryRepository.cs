using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Security
{
    public class SalaryCategoryRepository(AlphaBankDbContext context) : ISalaryCategoryRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<ICollection<SalaryCategory>> GetAllAsync()
            => await _context.SalaryCategories.ToListAsync();
    }
}