using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class EmployeeMapper {

        public static Person MapPerson(CreateEmployeeDto oCreateEmployeeDto)
            => new() {
                Id = (int) oCreateEmployeeDto.PersonId!,
                Name = oCreateEmployeeDto.Name,
                FirstName = oCreateEmployeeDto.FirstName,
                SecondName = oCreateEmployeeDto.SecondName,
                DateBirth = (DateOnly)oCreateEmployeeDto.DateBirth!,
                Address = oCreateEmployeeDto.Address,
                TypeIdentificationId = (byte) oCreateEmployeeDto.TypeIdentificationId!,
                MaritalStatusId = (byte) oCreateEmployeeDto.MaritalStatusId!,
                NationalityId = (byte) oCreateEmployeeDto.NationalityId!,
            };

        public static Employee MapEmployee(CreateEmployeeDto oCreateEmployeeDto)
            => new() {
                DateEntry = (DateOnly) oCreateEmployeeDto.DateEntry!,
                SalaryCategoryId = (byte) oCreateEmployeeDto.SalaryCategoryId!
            };

        public static ShowEmployeeDto MapShowEmployeeDto(Employee oEmployee)
            => new() {
                Status = oEmployee.Status,
                Person = {
                    Name = oEmployee.Person.Name,
                    FirstName = oEmployee.Person.FirstName,
                    SecondName = oEmployee.Person.SecondName,
                    PhoneNumber = oEmployee.Person.Phones.FirstOrDefault()!.Number        
                },
                User = {
                    EmailAddress = oEmployee.Users.FirstOrDefault()!.EmailAddress,
                    Role = oEmployee.Users.FirstOrDefault()!.Role.Description
                }
            };
    }
}