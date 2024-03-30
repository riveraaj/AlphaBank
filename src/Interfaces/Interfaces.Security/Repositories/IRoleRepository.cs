using Data.AlphaBank;

namespace Interfaces.Security.Repositories {
    public interface IRoleRepository {

        public Task<ICollection<Role>> GetAllAsync();

        public Task CreateAsync(Role oRole);

        public Task SaveChangesAsync();
    }
}