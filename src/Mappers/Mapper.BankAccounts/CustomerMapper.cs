
using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Mapper.BankAccounts {
    public static class CustomerMapper {

        public static Customer MapCustomer(CreateCustomerDto oCreateCustomerDto)
            => new() {
                EmailAddress = oCreateCustomerDto.EmailAddress, 
                AverageMonthlySalary = (decimal) oCreateCustomerDto.AverageMonthlySalary!,
                PersonId = (int) oCreateCustomerDto.Person.PersonId!,
                OccupationId = (byte) oCreateCustomerDto.OccupationId!
            };

        public static ShowCustomerDto MapShowCustomerDto(Customer oCustomer)
        {
            var showCustomerDto = new ShowCustomerDto
            {
                Status = oCustomer.Status,
                EmailAddress = oCustomer.EmailAddress,
                AverageMonthlySalary = oCustomer.AverageMonthlySalary,
                Occupation = oCustomer.Occupation.Description,
                CustomerStatus = oCustomer.CustomerStatus.Description,
                Person = new ShowPersonDto
                {
                    Name = oCustomer.Person.Name,
                    FirstName = oCustomer.Person.FirstName,
                    SecondName = oCustomer.Person.SecondName
                }
            };

            if (oCustomer.Person.Phones.Any())
                showCustomerDto.Person.PhoneNumber = oCustomer.Person.Phones.First().Number;

            return showCustomerDto;
        }
    }
}