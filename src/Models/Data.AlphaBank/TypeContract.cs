namespace Data.AlphaBank;

public partial class TypeContract {
    public byte Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}