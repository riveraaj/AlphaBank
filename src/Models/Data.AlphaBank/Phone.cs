namespace Data.AlphaBank;

public partial class Phone {
    public int Id { get; set; }
    public int Number { get; set; }
    public int PersonId { get; set; }
    public byte TypePhoneId { get; set; }
    public virtual Person Person { get; set; } = null!;
    public virtual TypePhone TypePhone { get; set; } = null!;
}