namespace Data.AlphaBank;
public partial class Employee {
    public int Id { get; set; }

    public bool Status { get; set; }

    public DateOnly DateEntry { get; set; }

    public int PersonId { get; set; }

    public byte SalaryCategoryId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual SalaryCategory SalaryCategory { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}