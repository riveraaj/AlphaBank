using Data.AlphaBank;

namespace Interfaces.Common {
    public interface INationality {

        public Task<ICollection<Nationality>> GetAllAsync();
    }
}