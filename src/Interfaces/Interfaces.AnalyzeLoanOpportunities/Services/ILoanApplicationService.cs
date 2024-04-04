using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface ILoanApplicationService  {
        public Task<List<ShowLoanApplicationDTO>> GetAllForGrantLoan();
        public Task<bool> Create(CreateLoanApplicationDTO oCreateLoanApplicationDTO);
        public byte[] ConvertToPdf(FileUploadDTO fileUploadDTO, string title);
    }
}