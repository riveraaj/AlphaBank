using Data.AlphaBank;

namespace Interfaces.Security {
    public interface ISalaryCategory {

        public Task<ICollection<SalaryCategory>> GetAllAsync();
    }
}