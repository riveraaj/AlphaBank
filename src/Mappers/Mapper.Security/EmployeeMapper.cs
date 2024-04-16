using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class EmployeeMapper {

        public static Employee MapUpdateEmployee(UpdateEmployeeDTO oUpdateEmployeeDTO)
            => new() {
                Id = oUpdateEmployeeDTO.EmployeeId,
                SalaryCategoryId = (byte) oUpdateEmployeeDTO.SalaryCategoryId!,
            };

        public static UpdateEmployeeDTO MapUpdateEmployeeDTOForShow(Employee oEmployee)
            => new() { 
                EmployeeId = oEmployee.Id,
                PersonId = oEmployee.PersonId,
                PhoneNumber = oEmployee.Person.Phones.LastOrDefault()!.Number,
                TypePhoneId = oEmployee.Person.Phones.LastOrDefault()!.TypePhoneId,
                SalaryCategoryId = oEmployee.SalaryCategoryId
            };

        public static Employee MapEmployee(CreateEmployeeDTO oCreateEmployeeDTO)
            => new() {
                PersonId = (int) oCreateEmployeeDTO.Person!.PersonId!,
                DateEntry = (DateOnly) oCreateEmployeeDTO.DateEntry!,
                SalaryCategoryId = (byte) oCreateEmployeeDTO.SalaryCategoryId!
            };

        public static ShowEmployeeDTO MapShowEmployeeDTO(Employee oEmployee) {
            var showEmployeeDto = new ShowEmployeeDTO {
                Id = oEmployee.Id.ToString(),
                Status = oEmployee.Status,
                PersonId = oEmployee.PersonId.ToString(),
                Person = new ShowPersonDTO {
                    Name = oEmployee.Person.Name,
                    FirstName = oEmployee.Person.FirstName,
                    SecondName = oEmployee.Person.SecondName
                },
                User = new ShowUserDTO()
            };

            if (oEmployee.Person.Phones.Count != 0)
                showEmployeeDto.Person.PhoneNumber = oEmployee.Person.Phones.Last().Number;
            
            if (oEmployee.Users.Count != 0)  {
                var firstUser = oEmployee.Users.First();
                showEmployeeDto.User.EmailAddress = firstUser.EmailAddress;

                if (firstUser.Role != null)
                    showEmployeeDto.User.Role = firstUser.Role.Description; 
            }

            return showEmployeeDto;
        }
    }
}