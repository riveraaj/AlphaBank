using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common {
    public static class PhoneMapper {

        public static Phone MapPhone(CreatePhoneDTO oCreatePhoneDTO)
            => new() {
                Number = (int) oCreatePhoneDTO.PhoneNumber!,
                PersonId = oCreatePhoneDTO.PersonId,
                TypePhoneId = (byte) oCreatePhoneDTO.TypePhoneId!
            };
    }
}