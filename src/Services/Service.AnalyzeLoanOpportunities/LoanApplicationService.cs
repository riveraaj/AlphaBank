using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Repositories;
using Mapper.AnalyzeLoanOpportunities;
using Microsoft.Extensions.Logging;
using Service.AnalyzeLoanOpportunities.Helper;

namespace Service.AnalyzeLoanOpportunities {
    public class LoanApplicationService(ILoanApplicationRepository loanApplicationRepository,
                                        IAccountRepository accountRepository,
                                        ILogger<LoanApplicationService> logger): ILoanApplicationService {

        private readonly ILoanApplicationRepository _loanApplicationRepository
            = loanApplicationRepository;

        private readonly IAccountRepository _accountRepository = accountRepository;

        private readonly ILogger<LoanApplicationService> _logger = logger;

        public async Task<bool> Create(CreateLoanApplicationDTO oCreateLoanApplicationDTO) {
            try {
                // Create loan application
                var loanApplication = LoanApplicationMapper.MapLoanApplication(oCreateLoanApplicationDTO);
                loanApplication.DateApplication = DateOnly.FromDateTime(DateTime.Now);
                loanApplication.ApplicationStatusId = 1;

                // Get account associated with the loan application
                var account = await _accountRepository.GetByIdForLoanApplication(loanApplication.AccountId);

                // Create titles for the documents
                string employerOrderTitle = $"EmployerOrder-{account!.Customer.PersonId}-{loanApplication.DateApplication}";
                string salaryStatementTitle = $"SalaryStatement-{account.Customer.PersonId}-{loanApplication.DateApplication}";

                // Convert files to PDF and save them to the file system
                byte[] employerOrder = ConvertToPdf(oCreateLoanApplicationDTO.EmployerOrder, employerOrderTitle);
                byte[] salaryStatement = ConvertToPdf(oCreateLoanApplicationDTO.SalaryStatement, salaryStatementTitle);

                string pathEmployerOrder = SaveFileHelper.SaveFile(employerOrder, employerOrderTitle);
                string pathSalaryStatement = SaveFileHelper.SaveFile(salaryStatement, salaryStatementTitle);

                // Update document paths in the loan application
                loanApplication.PathPatronalOrder = pathEmployerOrder;
                loanApplication.PathProofSalary = pathSalaryStatement;

                _logger.LogInformation("----- Create Loan Application: Start the creation of a loan application registry");

                // Save loan application
                await _loanApplicationRepository.Create(loanApplication);
                await _loanApplicationRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Loan Application: Creation completed and saved successfully.");

                return true;
            }
            catch (Exception e)  {
                // Handle errors and log them appropriately
                _logger.LogError($"----- Create Loan Application: An error occurred while creating and saving to the database. More about error: {e.Message}");
                return false;
            }
        }

        public byte[] ConvertToPdf(FileUploadDTO oFileUploadDTO, string title) {

            string fileExtension = Path.GetExtension(oFileUploadDTO.ContentType);

            return fileExtension.ToLower() switch {
                ".txt" => FileConverterHelper.ConvertTxtToPdf(oFileUploadDTO, title),
                ".doc" or ".docx" => FileConverterHelper.ConvertWordToPdf(oFileUploadDTO, title),
                _ => FileConverterHelper.CopyPdf(oFileUploadDTO, title),// Convert to PDF by default if the file extension is not supported
            };
        }
    }
}