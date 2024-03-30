using Dtos.AlphaBank.AnalyzeLoanOpportunities;

namespace Interfaces.AnalyzeLoanOpportunities.Services {
    public interface ILoanApplicationService  {

        public Task<bool> Create(CreateLoanApplicationDto oCreateLoanApplicationDto);

        public byte[] ConvertToPdf(FileUploadDto fileUploadDto, string title);
    }
}