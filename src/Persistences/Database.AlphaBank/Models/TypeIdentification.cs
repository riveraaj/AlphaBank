using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class TypeIdentification
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
