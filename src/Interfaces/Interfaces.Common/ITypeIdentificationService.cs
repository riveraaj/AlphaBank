using Data.AlphaBank;

namespace Interfaces.Common { 
    public interface ITypeIdentificationService {

        public Task<List<TypeIdentification>> GetAll();
    }
}