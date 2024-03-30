namespace Data.AlphaBank;
public partial class Account {
    public string Id { get; set; } = null!;
    public bool Status { get; set; }
    public decimal Balance { get; set; }
    public DateOnly DateOpening { get; set; }
    public int CustomerId { get; set; }
    public byte TypeAccountId { get; set; }
    public byte TypeCurrencyId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
    public virtual TypeAccount TypeAccount { get; set; } = null!;
    public virtual TypeCurrency TypeCurrency { get; set; } = null!;
}