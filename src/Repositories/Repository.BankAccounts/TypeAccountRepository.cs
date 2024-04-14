
using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.BankAccounts {
    public class TypeAccountRepository (AlphaBankDbContext context) : ITypeAccountRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(TypeAccount oTypeAccount)
            => await _context.TypeAccounts.AddAsync(oTypeAccount);

        public async Task<ICollection<TypeAccount>> GetAllAsync()
            => await _context.TypeAccounts.ToListAsync();

        public async Task UpdateAsync(TypeAccount oTypeAccount)
        {
            var typeAccount = await _context.TypeAccounts.FirstOrDefaultAsync(x => x.Id == oTypeAccount.Id);

            if (typeAccount == null) return;

            typeAccount.Description = oTypeAccount.Description;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var typeAccount = await _context.TypeAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (typeAccount != null) _context.TypeAccounts.Remove(typeAccount);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}