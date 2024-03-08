using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte RoleId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();

    public virtual Role Role { get; set; } = null!;
}
