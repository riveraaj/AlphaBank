using Data.AlphaBank;

namespace Interfaces.Common.Services {
    public interface IMaritalStatusService {

        public Task<List<MaritalStatus>> GetAll();
    }
}