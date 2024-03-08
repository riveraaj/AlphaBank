using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class CollectionStatus
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<CollectionHistory> CollectionHistories { get; set; } = new List<CollectionHistory>();
}
