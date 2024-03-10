using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IRoleRepository {

        public Task<ICollection<Role>> GetAllAsync();

        public Task CreateAsync(Role oRole);

    }
}