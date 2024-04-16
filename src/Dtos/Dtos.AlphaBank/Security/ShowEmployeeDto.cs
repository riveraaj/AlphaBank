using Dtos.AlphaBank.Common;

namespace Dtos.AlphaBank.Security {
    public class ShowEmployeeDTO {
        public string Id { get; set; } = null!;
        public bool Status { get; set; }
        public string PersonId { get; set; } = null!;
        public ShowPersonDTO Person { get; set; } = null!;
        public ShowUserDTO User { get; set; } = null!;
    }
}