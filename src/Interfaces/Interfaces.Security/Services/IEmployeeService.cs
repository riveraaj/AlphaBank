using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IEmployeeService {
        public Task<List<ShowEmployeeDTO>> GetAll();
        public Task<List<Employee>> GetAllForUser();
        public Task<bool> Create(CreateEmployeeDTO oCreateEmployeeDTO);
    }
}