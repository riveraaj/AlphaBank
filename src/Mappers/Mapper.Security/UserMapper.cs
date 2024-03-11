using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class UserMapper {

        public static User MapUser(CreateUserDto oCreateUserDto)
            => new() {
                EmailAddress = oCreateUserDto.Email,
                Password = oCreateUserDto.Password,
                RoleId = (byte) oCreateUserDto.RoleId!,
                EmployeeId = (int) oCreateUserDto.EmployeeId!
            };

    }
}