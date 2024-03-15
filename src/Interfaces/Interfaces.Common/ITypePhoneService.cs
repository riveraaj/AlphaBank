using Data.AlphaBank;

namespace Interfaces.Common {
    public interface ITypePhoneService {

        public Task<List<TypePhone>> GetAll();
    }
}