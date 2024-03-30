using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common {
    public static class PhoneMapper {

        public static Phone MapPhone(CreatePhoneDto oCreatePhoneDto)
            => new() {
                Number = (int) oCreatePhoneDto.PhoneNumber!,
                PersonId = oCreatePhoneDto.PersonId,
                TypePhoneId = (byte) oCreatePhoneDto.TypePhoneId!
            };
    }
}