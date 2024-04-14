
using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class TypeCurrencyRepository (AlphaBankDbContext context) : ITypeCurrencyRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(TypeCurrency oTypeCurrency)
            => await _context.TypeCurrencies.AddAsync(oTypeCurrency);

        public async Task<ICollection<TypeCurrency>> GetAllAsync()
            => await _context.TypeCurrencies.ToListAsync();

        public async Task UpdateAsync(TypeCurrency oTypeCurrency)
        {
            var typeCurrency = await _context.TypeCurrencies.FirstOrDefaultAsync(x => x.Id == oTypeCurrency.Id);

            if (typeCurrency == null) return;

            typeCurrency.Description = oTypeCurrency.Description;
        }

        public async Task RemoveAsync(int id)
        {
            //Search for the record in the table 
            var typeCurrency = await _context.TypeCurrencies.FirstOrDefaultAsync(x => x.Id == id);

            if (typeCurrency != null) _context.TypeCurrencies.Remove(typeCurrency);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}