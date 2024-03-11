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

    }
}