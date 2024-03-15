using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface IPhoneRepository
    {

        public Task CreateAsync(Phone oPhone);
    }
}