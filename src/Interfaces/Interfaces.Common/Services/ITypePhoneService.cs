using Data.AlphaBank;

namespace Interfaces.Common.Services
{
    public interface ITypePhoneService
    {

        public Task<List<TypePhone>> GetAll();
    }
}