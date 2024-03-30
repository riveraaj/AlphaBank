using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface ITypeContractRepository
    {

        public Task<ICollection<TypeContract>> GetAllAsync();

    }
}
