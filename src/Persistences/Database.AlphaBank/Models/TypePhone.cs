using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class TypePhone
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
