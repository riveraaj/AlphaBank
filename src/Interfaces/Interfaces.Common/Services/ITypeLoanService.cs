using Data.AlphaBank;

namespace Interfaces.Common.Services
{
    public interface ITypeLoanService
    {
        public Task<List<TypeLoan>> GetAll();

    }
}
