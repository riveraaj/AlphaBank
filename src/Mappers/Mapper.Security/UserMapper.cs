using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class UserMapper {

        public static ShowUserDTO MapShowUserDTO(User oUser)
            => new() {
                Id = oUser.Id.ToString(),
                EmailAddress = oUser.EmailAddress,
                Role = oUser.Role.Description,
                Person = new() {
                    FirstName = oUser.Employee.Person.FirstName,
                    Name = oUser.Employee.Person.Name,
                    PhoneNumber = oUser.Employee.Person.Phones.FirstOrDefault()!.Number,
                    SecondName = oUser.Employee.Person.SecondName
                }
            };

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
                //Password = oUpdateUserDTO.Password,
                RoleId = (byte) oUpdateUserDTO.RoleId!
            };

        public static UpdateUserDTO MapUpdateUser(User oUser)
           => new() {
               Id = oUser.Id,
               Email = oUser.EmailAddress,
               //Password = oUser.Password,
               RoleId = oUser.RoleId!
           };
    }
}