using Data.AlphaBank;
using Database.AlphaBank;

namespace Repository.Security {
    public class PersonRepository(AlphaBankDbContext context)  {

        private readonly AlphaBankDbContext _context = context;

        public async Task Create(Person oPerson) 
            => await _context.People.AddAsync(oPerson);
    }
}