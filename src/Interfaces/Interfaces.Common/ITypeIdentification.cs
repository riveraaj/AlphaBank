using Data.AlphaBank;

namespace Interfaces.Common {
    public interface ITypeIdentification {
        public Task<ICollection<TypeIdentification>> GetAllAsync();
    }
}