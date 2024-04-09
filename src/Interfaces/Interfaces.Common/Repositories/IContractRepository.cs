using Data.AlphaBank;

namespace Interfaces.Common.Repositories {
    public interface IContractRepository {
        public Task<ICollection<Contract>> GetAllAsync();
        public Task CreateAsync(Contract oContract);
        public Task<ICollection<Contract>> GetByLoanApplicationId(int id);
        public Task<Contract?> GetByLoanApplicationID(int id);
        public Task SaveChangesAsync();
    }
}