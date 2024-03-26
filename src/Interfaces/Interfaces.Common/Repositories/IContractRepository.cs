using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface IContractRepository
    {

        public Task<ICollection<Contract>> GetAllAsync();

        public Task CreateAsync(Contract oContract);

        public Task SaveChangesAsync();

    }
}
