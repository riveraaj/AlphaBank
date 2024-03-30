using Data.AlphaBank;

namespace Interfaces.Common.Services
{
    public interface ITypeIdentificationService
    {

        public Task<List<TypeIdentification>> GetAll();
    }
}