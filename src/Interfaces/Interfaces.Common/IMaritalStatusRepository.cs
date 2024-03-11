using Data.AlphaBank;

namespace Interfaces.Common {
    public interface IMaritalStatusRepository {

        public Task<ICollection<MaritalStatus>> GetAllAsync();
    }
}