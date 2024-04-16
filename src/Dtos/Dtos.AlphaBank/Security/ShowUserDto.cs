using Dtos.AlphaBank.Common;

namespace Dtos.AlphaBank.Security {
    public class ShowUserDTO {
        public string Id { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Role { get; set; } = null!;
        public ShowPersonDTO Person { get; set; } = null!;
    }
}