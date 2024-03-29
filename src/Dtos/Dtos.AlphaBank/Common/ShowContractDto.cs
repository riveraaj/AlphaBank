namespace Dtos.AlphaBank.Common
{
    public class ShowContractDto
    {
        public string TypeContractDescription { get; set; } = null!;

        public DateOnly DateStart { get; set; }

        public DateOnly DateCompletion { get; set; }

        public DateOnly DateUpdate { get; set; }

        public string PathFile { get; set; } = null!;

    }
}
