using Data.AlphaBank;

namespace Interfaces.Common {
    public interface IMaritalStatus {

        public Task<ICollection<MaritalStatus>> GetAllAsync();
    }
}