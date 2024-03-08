namespace Data.AlphaBank;

public partial class Notification {
    public int Id { get; set; }

    public DateOnly DateShipment { get; set; }

    public int LoanId { get; set; }

    public byte TypeNotificationId { get; set; }

    public virtual Loan Loan { get; set; } = null!;

    public virtual TypeNotification TypeNotification { get; set; } = null!;
}