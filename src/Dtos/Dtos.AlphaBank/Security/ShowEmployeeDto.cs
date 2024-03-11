namespace Dtos.AlphaBank.Security {
    public class ShowEmployeeDto {

        public bool Status { get; set; }

        public ShowPersonDto Person { get; set; } = null!;

        public ShowUserDto User { get; set; } = null!;
    }
}