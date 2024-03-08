namespace Data.AlphaBank;

public partial class Customer {
    public int Id { get; set; }

    public bool Status { get; set; }

    public string EmailAddress { get; set; } = null!;

    public decimal AverageMonthlySalary { get; set; }

    public int PersonId { get; set; }

    public byte OccupationId { get; set; }

    public byte CustomerStatusId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual CustomerStatus CustomerStatus { get; set; } = null!;

    public virtual Occupation Occupation { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}