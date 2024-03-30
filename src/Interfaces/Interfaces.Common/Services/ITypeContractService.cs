using Data.AlphaBank;

namespace Interfaces.Common.Services {
    public interface ITypeContractService {

        public Task<List<TypeContract>> GetAll();
    }
}