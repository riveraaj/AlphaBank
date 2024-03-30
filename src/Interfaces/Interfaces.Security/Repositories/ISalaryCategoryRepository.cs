using Data.AlphaBank;

namespace Interfaces.Security.Repositories {
    public interface ISalaryCategoryRepository {

        public Task<ICollection<SalaryCategory>> GetAllAsync();
    }
}