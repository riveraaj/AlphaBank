using Data.AlphaBank;

namespace Interfaces.Common.Repositories
{
    public interface ITypeLoanRepository
    {

        public Task<ICollection<TypeLoan>> GetAllAsync();

    }
}
