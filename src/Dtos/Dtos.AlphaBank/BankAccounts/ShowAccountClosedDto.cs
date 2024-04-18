namespace Dtos.AlphaBank.BankAccounts
{
    public class ShowAccountClosedDTO
    {
        public string Id { get; set; } = null!;
        public string TypeAccountDescription { get; set; } = null!;
        public string TypeCurrencyDescription { get; set; } = null!;
        public int PersonId { get; set; }
        public DateOnly DateOpening { get; set; }
    }
}
