using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface IMaritalStatusRepository {
        public Task<ICollection<MaritalStatus>> GetAllAsync();
    }
}