
using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;

namespace Mapper.BankAccounts
{
    public static class CustomerMapper
    {

        public static Customer MapCustomer(CreateCustomerDto oCreateCustomerDto)
            => new()
            {
                EmailAddress = oCreateCustomerDto.EmailAddress, 
                AverageMonthlySalary = (decimal) oCreateCustomerDto.AverageMonthlySalary,
                PersonId = (int) oCreateCustomerDto.Id,
                OccupationId = (byte) oCreateCustomerDto.OccupationId,
                CustomerStatusId = (byte)oCreateCustomerDto.CustomerStatusId
            };

        public static Person MapPerson(CreateCustomerDto oCreateCustomerDto)
            => new()
            {
                Id = (int) oCreateCustomerDto.Id,
                Name = oCreateCustomerDto.Name,
                FirstName = oCreateCustomerDto.FirstName,
                SecondName = oCreateCustomerDto.SecondName,
                DateBirth = (DateOnly) oCreateCustomerDto.DateBirth,
                Address = oCreateCustomerDto.Address,
                TypeIdentificationId = (byte)oCreateCustomerDto.TypeIdentificationId,
                NationalityId = (byte)oCreateCustomerDto.NationalityId,
                MaritalStatusId = (byte)oCreateCustomerDto.MaritalStatusId
            };

        public static Phone MapPhone(CreateCustomerDto oCreateCustomerDto)
            => new()
            {
                Number = (int)oCreateCustomerDto.Number,
                PersonId = (int)oCreateCustomerDto.Id,
                TypePhoneId = (byte)oCreateCustomerDto.TypePhoneId
            };

    }
}
