using Data.AlphaBank;

namespace Interfaces.Security {
    public interface ISalaryCategoryService {

        public Task<List<SalaryCategory>> GetAll();
    }
}