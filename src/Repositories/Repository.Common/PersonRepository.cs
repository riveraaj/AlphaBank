using Data.AlphaBank;
using Database.AlphaBank;
using Interfaces.Security;

namespace Repository.Security {
    public class PersonRepository(AlphaBankDbContext context) : IPersonRepository {

        private readonly AlphaBankDbContext _context = context;

        public async Task CreateAsync(Person oPerson) 
            => await _context.People.AddAsync(oPerson);
    }
}