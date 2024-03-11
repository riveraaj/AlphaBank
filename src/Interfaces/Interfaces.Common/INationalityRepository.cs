using Data.AlphaBank;

namespace Interfaces.Common {
    public interface INationalityRepository {

        public Task<ICollection<Nationality>> GetAllAsync();
    }
}