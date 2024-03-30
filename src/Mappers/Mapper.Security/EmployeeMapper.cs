using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class EmployeeMapper {
        public static Employee MapEmployee(CreateEmployeeDTO oCreateEmployeeDTO)
            => new() {
                PersonId = (int) oCreateEmployeeDTO.Person!.PersonId!,
                DateEntry = (DateOnly) oCreateEmployeeDTO.DateEntry!,
                SalaryCategoryId = (byte) oCreateEmployeeDTO.SalaryCategoryId!
            };

        public static ShowEmployeeDTO MapShowEmployeeDTO(Employee oEmployee) {
            var showEmployeeDto = new ShowEmployeeDTO {
                Status = oEmployee.Status,
                Person = new ShowPersonDTO
                {
                    Name = oEmployee.Person.Name,
                    FirstName = oEmployee.Person.FirstName,
                    SecondName = oEmployee.Person.SecondName
                },
                User = new ShowUserDTO()
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