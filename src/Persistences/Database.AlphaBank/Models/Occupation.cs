using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class Occupation
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
