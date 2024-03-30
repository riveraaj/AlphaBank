using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IEmployeeService {

        public Task<List<ShowEmployeeDTO>> GetAll();

        public Task<bool> Create(CreateEmployeeDTO oCreateEmployeeDTO);
    }
}