using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class Message
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<TypeNotification> TypeNotifications { get; set; } = new List<TypeNotification>();
}
