using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;

namespace Interfaces.Security.Services
{
    public interface IUserService
    {

        public Task<bool> UserSetup(CreateEmployeeDto oCreateEmployeeDto, IEmployeeRepository oEmployeeRepository);
        public Task<bool> Create(CreateUserDto oCreateUserDto);
    }
}