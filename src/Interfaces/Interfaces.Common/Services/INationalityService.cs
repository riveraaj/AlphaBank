using Data.AlphaBank;

namespace Interfaces.Common.Services {
    public interface INationalityService {
        public Task<List<Nationality>> GetAll();
    }
}