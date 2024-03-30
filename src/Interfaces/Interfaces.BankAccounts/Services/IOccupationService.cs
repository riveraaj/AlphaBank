using Data.AlphaBank;

namespace Interfaces.BankAccounts.Services
{
    public interface IOccupationService
    {

        public Task<List<Occupation>> GetAll();
    }
}