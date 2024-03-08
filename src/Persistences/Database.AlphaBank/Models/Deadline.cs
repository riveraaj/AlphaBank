using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class Deadline
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
}
