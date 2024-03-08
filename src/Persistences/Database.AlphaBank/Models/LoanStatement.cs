using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class LoanStatement
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
