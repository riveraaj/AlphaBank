using Data.AlphaBank;

namespace Interfaces.Common {
    public interface INationalityService {

        public Task<List<Nationality>> GetAll();
    }
}