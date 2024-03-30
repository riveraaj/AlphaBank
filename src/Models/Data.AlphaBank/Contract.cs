namespace Data.AlphaBank;

public partial class Contract {
    public int Id { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateCompletion { get; set; }
    public DateOnly DateUpdate { get; set; }
    public string PathFile { get; set; } = null!;
    public int LoanApplicationId { get; set; }
    public byte TypeContractId { get; set; }
    public virtual LoanApplication LoanApplication { get; set; } = null!;
    public virtual TypeContract TypeContract { get; set; } = null!;
}