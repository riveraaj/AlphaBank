using Data.AlphaBank;
using Database.AlphaBank;

namespace Repository.Common {
    public class PhoneRepository(AlphaBankDbContext context) {

        private readonly AlphaBankDbContext _context = context;

        public async Task Create(Phone oPhone)
            => await _context.Phones.AddAsync(oPhone);

    }
}