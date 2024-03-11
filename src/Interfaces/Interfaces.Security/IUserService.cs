using Dtos.AlphaBank.Security;

namespace Interfaces.Security {
    public  interface IUserService {

        public Task<bool> Create(CreateUserDto oCreateUserDto);
    }
}