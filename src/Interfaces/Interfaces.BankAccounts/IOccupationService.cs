using Data.AlphaBank;

namespace Interfaces.BankAccounts {
    public interface IOccupationService {

        public Task<List<Occupation>> GetAll();
    }
}