using Data.AlphaBank;

namespace Interfaces.BankAccounts
{
    public interface IOccupationRepository  {

        public Task<ICollection<Occupation>> GetAllAsync();

    }
}
