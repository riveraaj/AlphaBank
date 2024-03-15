using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IPhoneRepository {

        public Task CreateAsync(Phone oPhone);
    }
}