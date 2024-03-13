using Data.AlphaBank;

namespace Interfaces.BankAccounts
{
    public interface ICustomerStatusRepository
    {

        public Task<ICollection<CustomerStatus>> GetAllAsync();

    }
}
