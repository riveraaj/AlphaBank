namespace Data.AlphaBank;
public partial class ApplicationStatus {
    public byte Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
}