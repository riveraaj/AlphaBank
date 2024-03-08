namespace Data.AlphaBank;

public partial class TypeNotification {
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public byte MessageId { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}