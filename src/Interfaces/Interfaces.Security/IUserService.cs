using Dtos.AlphaBank.Security;

namespace Interfaces.Security {
    public  interface IUserService {

        public Task<bool> UserSetup(CreateEmployeeDto oCreateEmployeeDto, IEmployeeRepository oEmployeeRepository);
        public Task<bool> Create(CreateUserDto oCreateUserDto);
    }
}