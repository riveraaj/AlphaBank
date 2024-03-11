using Data.AlphaBank;

namespace Interfaces.Security {
    public interface IEmployee {

        public Task CreateAsync(Employee oEmployee);
    }
}