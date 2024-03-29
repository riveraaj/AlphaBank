
using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;
using System.Globalization;

namespace Mapper.BankAccounts {
    public static class CustomerMapper {

        public static ShowCustomerLoanDto MapShowCustomerLoan(Customer oCustomer)
            => new() {
                Age = DateTime.Today.Year - oCustomer.Person.DateBirth.Year,
                Address = oCustomer.Person.Address,
                AverageMonthlySalary = oCustomer.AverageMonthlySalary.ToString("#,0", CultureInfo.InvariantCulture),
                Email = oCustomer.EmailAddress,
                FullName = $"{oCustomer.Person.Name} {oCustomer.Person.FirstName} {oCustomer.Person.SecondName}",
                MaritalStatus = oCustomer.Person.MaritalStatus.Description,
                Nationality = oCustomer.Person.Nationality.Description, 
                Occupation = oCustomer.Occupation.Description,
                PhoneNumber = oCustomer.Person.Phones.FirstOrDefault()!.Number
            };

        public static Customer MapCustomer(CreateCustomerDto oCreateCustomerDto)
            => new() {
                EmailAddress = oCreateCustomerDto.EmailAddress, 
                AverageMonthlySalary = (decimal) oCreateCustomerDto.AverageMonthlySalary!,
                PersonId = (int) oCreateCustomerDto.Person.PersonId!,
                OccupationId = (byte) oCreateCustomerDto.OccupationId!
            };

        public static ShowCustomerDto MapShowCustomerDto(Customer oCustomer)
         => new() {
             CustomerId = oCustomer.Id.ToString(),
             PersonId = oCustomer.PersonId,
             Age = DateTime.Today.Year - oCustomer.Person.DateBirth.Year,
             Address = oCustomer.Person.Address,
             Email = oCustomer.EmailAddress,
             FullName = $"{oCustomer.Person.Name} {oCustomer.Person.FirstName} {oCustomer.Person.SecondName}",
             MaritalStatus = oCustomer.Person.MaritalStatus.Description,
             Nationality = oCustomer.Person.Nationality.Description,
             Occupation = oCustomer.Occupation.Description,
             PhoneNumber = oCustomer.Person.Phones.FirstOrDefault()!.Number
         };
    }
}