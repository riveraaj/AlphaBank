using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common {
    public static class PersonMapper {

        public static Person MapPerson(CreatePersonDTO oCreatePersonDTO)
            => new() {
                Address = oCreatePersonDTO.Address,
                DateBirth = (DateOnly) oCreatePersonDTO.DateBirth!,
                FirstName = oCreatePersonDTO.FirstName,
                MaritalStatusId = (byte) oCreatePersonDTO.MaritalStatusId!,
                NationalityId = (byte) oCreatePersonDTO.NationalityId!,
                Id = (int) oCreatePersonDTO.PersonId!,
                Name = oCreatePersonDTO.Name,
                SecondName = oCreatePersonDTO.SecondName,
                TypeIdentificationId = (byte) oCreatePersonDTO.TypeIdentificationId!
            };
    }
}