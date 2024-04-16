using Data.AlphaBank;

namespace Interfaces.Security.Repositories {
    public interface IRoleRepository {
        public Task<Role?> GetById(int id);
        public Task CreateAsync(Role oRole);
        public Task<ICollection<Role>> GetAllAsync();
        public Task UpdateAsync(Role oRole);
        public Task RemoveAsync(int id);
        public Task SaveChangesAsync();
    }
}