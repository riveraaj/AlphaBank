using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class Loan
{
    public int Id { get; set; }

    public int RemainingQuotas { get; set; }

    public int LoanApplicationId { get; set; }

    public byte LoanStatementId { get; set; }

    public virtual ICollection<CollectionHistory> CollectionHistories { get; set; } = new List<CollectionHistory>();

    public virtual LoanApplication LoanApplication { get; set; } = null!;

    public virtual LoanStatement LoanStatement { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
