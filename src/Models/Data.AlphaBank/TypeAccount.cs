namespace Data.AlphaBank;

public partial class TypeAccount {
    public byte Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}