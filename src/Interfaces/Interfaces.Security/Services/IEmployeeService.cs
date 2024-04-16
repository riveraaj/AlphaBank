using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services {
    public interface IEmployeeService {
        public Task<UpdateEmployeeDTO?> GetByIdForUpdate(int id);
        public Task<List<ShowEmployeeDTO>> GetAll();
        public Task<bool> Remove(int id);
        public Task<bool> Update(UpdateEmployeeDTO oUpdateEmployeeDTO);
        public Task<List<Employee>> GetAllForUser();
        public Task<bool> Create(CreateEmployeeDTO oCreateEmployeeDTO);
    }
}