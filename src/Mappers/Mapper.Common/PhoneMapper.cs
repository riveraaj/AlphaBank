using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Dtos.AlphaBank.Security;

namespace Mapper.Common {
    public static class PhoneMapper {

        public static Phone MapPhoneForEmployee(UpdateEmployeeDTO oUpdateEmployeeDTO)
            => new() {
                PersonId = oUpdateEmployeeDTO.PersonId,
                Number = (int)oUpdateEmployeeDTO.PhoneNumber!,
                TypePhoneId = (byte)oUpdateEmployeeDTO.TypePhoneId!,
            };

        public static Phone MapPhone(CreatePhoneDTO oCreatePhoneDTO)
            => new() {
                Number = (int) oCreatePhoneDTO.PhoneNumber!,
                PersonId = oCreatePhoneDTO.PersonId,
                TypePhoneId = (byte) oCreatePhoneDTO.TypePhoneId!
            };
    }
}