using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class UserMapper {

        public static User MapUser(CreateUserDTO oCreateUserDTO)
            => new() {
                EmailAddress = oCreateUserDTO.Email,
                Password = oCreateUserDTO.Password,
                RoleId = (byte) oCreateUserDTO.RoleId!,
                EmployeeId = (int) oCreateUserDTO.EmployeeId!
            };

        public static User MapUpdateUser(UpdateUserDTO oUpdateUserDTO)
            => new()
            {
                Id = oUpdateUserDTO.Id,
                EmailAddress = oUpdateUserDTO.Email,
                Password = oUpdateUserDTO.Password,
                RoleId = (byte) oUpdateUserDTO.RoleId!
            };
    }
}