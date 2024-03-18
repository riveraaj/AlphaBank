
using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts {
    public static class CustomerMapper {

        public static Customer MapCustomer(CreateCustomerDto oCreateCustomerDto)
            => new() {
                EmailAddress = oCreateCustomerDto.EmailAddress, 
                AverageMonthlySalary = (decimal) oCreateCustomerDto.AverageMonthlySalary!,
                PersonId = (int) oCreateCustomerDto.Person.PersonId!,
                OccupationId = (byte) oCreateCustomerDto.OccupationId!
            };
    }
}