namespace Data.AlphaBank;

public partial class CollectionHistory {
    public int Id { get; set; }
    public DateOnly Deadline { get; set; }
    public int QuotaNumber { get; set; }
    public decimal Amount { get; set; }
    public DateOnly DateDeposit { get; set; }
    public decimal DepositMount { get; set; }
    public decimal MoratoriumInterest { get; set; }
    public byte CollectionStatusId { get; set; }
    public int LoanId { get; set; }
    public virtual CollectionStatus CollectionStatus { get; set; } = null!;
    public virtual Loan Loan { get; set; } = null!;
}