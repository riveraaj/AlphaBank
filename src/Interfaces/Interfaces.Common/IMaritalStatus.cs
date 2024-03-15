using Data.AlphaBank;

namespace Interfaces.Common {
    public interface IMaritalStatus   {

        public Task<List<MaritalStatus>> GetAll();
    }
}