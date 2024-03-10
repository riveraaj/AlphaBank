using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IPersonRepository {

        public Task CreateAsync(Person oPerson);

    }
}