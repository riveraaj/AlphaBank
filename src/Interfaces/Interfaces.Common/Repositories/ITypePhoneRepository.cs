using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface ITypePhoneRepository
    {

        public Task<ICollection<TypePhone>> GetAllAsync();
    }
}