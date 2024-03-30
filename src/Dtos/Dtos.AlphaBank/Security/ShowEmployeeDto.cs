using Dtos.AlphaBank.Common;

namespace Dtos.AlphaBank.Security
{
    public class ShowEmployeeDTO {

        public bool Status { get; set; }

        public ShowPersonDTO Person { get; set; } = null!;

        public ShowUserDTO User { get; set; } = null!;
    }
}