using Dtos.AlphaBank.Security;

namespace Interfaces.Security.Services
{
    public interface IEmployeeService
    {

        public Task<List<ShowEmployeeDto>> GetAll();

        public Task<bool> Create(CreateEmployeeDto oCreateEmployeeDto);
    }
}