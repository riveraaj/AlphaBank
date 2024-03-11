using Data.AlphaBank;

namespace Interfaces.Security {
    public interface ISalaryCategoryRepository {

        public Task<ICollection<SalaryCategory>> GetAllAsync();
    }
}