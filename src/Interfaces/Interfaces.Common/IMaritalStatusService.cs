using Data.AlphaBank;

namespace Interfaces.Common {
    public interface IMaritalStatusService   {

        public Task<List<MaritalStatus>> GetAll();
    }
}