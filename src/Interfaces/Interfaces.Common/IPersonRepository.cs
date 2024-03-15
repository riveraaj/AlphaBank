using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IPersonRepository {

        public Task<Person?> GetByIdAsync(int id);

        public Task CreateAsync(Person oPerson);
    }
}