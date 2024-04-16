
using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.Common {
    public class TypeCurrencyRepository (AlphaBankDbContext context) : ITypeCurrencyRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task<TypeCurrency?> GetById(int id)
            => await _context.TypeCurrencies.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(TypeCurrency oTypeCurrency)
            => await _context.TypeCurrencies.AddAsync(oTypeCurrency);

        public async Task<ICollection<TypeCurrency>> GetAllAsync()
            => await _context.TypeCurrencies.ToListAsync();

        public async Task UpdateAsync(TypeCurrency oTypeCurrency) {
            try {
                var typeCurrency = await _context.TypeCurrencies.FirstOrDefaultAsync(x => x.Id == oTypeCurrency.Id)
                    ?? throw new InvalidOperationException("Deadline not found."); ;

                typeCurrency.Description = oTypeCurrency.Description;
            } catch (Exception e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task RemoveAsync(int id) {
            try {
                //Search for the record in the table 
                var typeCurrency = await _context.TypeCurrencies.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new InvalidOperationException("Deadline not found.");

                _context.TypeCurrencies.Remove(typeCurrency);
            } catch (SqlException e) {
                throw new Exception("Database error", e);
            }
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}