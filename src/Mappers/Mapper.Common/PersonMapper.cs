using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common {
    public static class PersonMapper {

        public static Person MapPerson(CreatePersonDto oCreatePersonDto)
            => new() {
                Address = oCreatePersonDto.Address,
                DateBirth = (DateOnly) oCreatePersonDto.DateBirth!,
                FirstName = oCreatePersonDto.FirstName,
                MaritalStatusId = (byte) oCreatePersonDto.MaritalStatusId!,
                NationalityId = (byte) oCreatePersonDto.NationalityId!,
                Id = (int) oCreatePersonDto.PersonId!,
                Name = oCreatePersonDto.Name,
                SecondName = oCreatePersonDto.SecondName,
                TypeIdentificationId = (byte) oCreatePersonDto.TypeIdentificationId!
            };
    }
}