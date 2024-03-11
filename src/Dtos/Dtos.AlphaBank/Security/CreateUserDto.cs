namespace Dtos.AlphaBank.Security {
    public class CreateUserDto {

        public string Email { get; set; } = null!; 
        public string Password { get; set; } = null!;

        public byte? RoleId { get; set; }

        public int? EmployeeId { get; set; }

    }
}