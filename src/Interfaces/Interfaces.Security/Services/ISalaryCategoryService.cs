using Data.AlphaBank;

namespace Interfaces.Security.Services {
    public interface ISalaryCategoryService {

        public Task<List<SalaryCategory>> GetAll();
    }
}