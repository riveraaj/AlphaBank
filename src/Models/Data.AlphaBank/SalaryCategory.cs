namespace Data.AlphaBank;

public partial class SalaryCategory {
    public byte Id { get; set; }
    public decimal Description { get; set; }
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}