using Data.AlphaBank;

namespace Interfaces.Common {
    public interface ITypeIdentificationRepository {
        public Task<ICollection<TypeIdentification>> GetAllAsync();
    }
}