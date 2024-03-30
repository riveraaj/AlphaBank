using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface INationalityRepository {
        public Task<ICollection<Nationality>> GetAllAsync();
    }
}