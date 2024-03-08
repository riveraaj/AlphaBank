using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class Role
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
