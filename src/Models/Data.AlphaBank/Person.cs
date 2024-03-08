namespace Data.AlphaBank;

public partial class Person {
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public bool Deceased { get; set; }

    public DateOnly DateBirth { get; set; }

    public string Address { get; set; } = null!;

    public byte TypeIdentificationId { get; set; }

    public byte NationalityId { get; set; }

    public byte MaritalStatusId { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual MaritalStatus MaritalStatus { get; set; } = null!;

    public virtual Nationality Nationality { get; set; } = null!;

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    public virtual TypeIdentification TypeIdentification { get; set; } = null!;
}