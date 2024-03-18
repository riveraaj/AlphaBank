using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface ITypeIdentificationRepository
    {
        public Task<ICollection<TypeIdentification>> GetAllAsync();
    }
}