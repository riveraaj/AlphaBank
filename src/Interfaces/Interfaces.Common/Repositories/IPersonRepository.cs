using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface IPersonRepository {

        public Task<Person?> GetByIdAsync(int id);

        public Task CreateAsync(Person oPerson);
    }
}