using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class EmployeeMapper {
        public static Employee MapEmployee(CreateEmployeeDto oCreateEmployeeDto)
            => new() {
                PersonId = (int) oCreateEmployeeDto.Person!.PersonId!,
                DateEntry = (DateOnly) oCreateEmployeeDto.DateEntry!,
                SalaryCategoryId = (byte) oCreateEmployeeDto.SalaryCategoryId!
            };

        public static ShowEmployeeDto MapShowEmployeeDto(Employee oEmployee) {
            var showEmployeeDto = new ShowEmployeeDto {
                Status = oEmployee.Status,
                Person = new ShowPersonDto {
                    Name = oEmployee.Person.Name,
                    FirstName = oEmployee.Person.FirstName,
                    SecondName = oEmployee.Person.SecondName
                },
                User = new ShowUserDto()
            };

            if (oEmployee.Person.Phones.Any())
                showEmployeeDto.Person.PhoneNumber = oEmployee.Person.Phones.First().Number;
            
            if (oEmployee.Users.Any())  {
                var firstUser = oEmployee.Users.First();
                showEmployeeDto.User.EmailAddress = firstUser.EmailAddress;

                if (firstUser.Role != null)
                    showEmployeeDto.User.Role = firstUser.Role.Description; 
            }

            return showEmployeeDto;
        }
    }
}