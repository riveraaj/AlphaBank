using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Common.Repositories;

namespace Repository.Common
{
    public class PhoneRepository(AlphaBankDbContext context) : IPhoneRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Phone oPhone)
            => await _context.Phones.AddAsync(oPhone);
    }
}