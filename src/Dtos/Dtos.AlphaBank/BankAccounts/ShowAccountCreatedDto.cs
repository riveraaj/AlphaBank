namespace Dtos.AlphaBank.BankAccounts
{
    public class ShowAccountCreatedDTO
    {
        public string Id { get; set; } = null!;
        public string TypeAccountDescription { get; set; } = null!;
        public string TypeCurrencyDescription { get; set; } = null!;
        public int PersonId { get; set; }
        public string Balance { get; set; } = null!;
        public DateOnly DateOpening { get; set; }
    }
}
