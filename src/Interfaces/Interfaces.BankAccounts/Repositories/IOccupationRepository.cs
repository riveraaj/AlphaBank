using Data.AlphaBank;

namespace Interfaces.BankAccounts.Repositories {
    public interface IOccupationRepository {
        public Task<ICollection<Occupation>> GetAllAsync();
    }
}