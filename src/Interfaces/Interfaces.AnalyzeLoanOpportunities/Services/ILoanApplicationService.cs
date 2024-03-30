using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface ILoanApplicationService  {

        public Task<bool> Create(CreateLoanApplicationDTO oCreateLoanApplicationDTO);

        public byte[] ConvertToPdf(FileUploadDTO oFileUploadDTO, string title);
    }
}