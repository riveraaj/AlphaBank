using Data.AlphaBank;

namespace Interfaces.Security {
    public interface ITypePhoneRepository {

        public Task<ICollection<Phone>> GetAllAsync();

    }
}