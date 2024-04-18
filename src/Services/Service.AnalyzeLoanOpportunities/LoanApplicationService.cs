using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;
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

        public async Task<List<ShowLoanApplicationDTO>> GetAllForGrantLoan() {
            try {
                //Attempt to retrieve all loan application asynchronously from the AccountRepository.
                var loanApplicationList = await _loanApplicationRepository.GetAllAsync();

                var filteredList = loanApplicationList.Where(x => x.ApplicationStatusId == 1);

                //Initialize a list to store ShowLoanApplicationDTO objects.
                var finalList = new List<ShowLoanApplicationDTO>();

                //Map each loan application to a ShowLoanApplicationDTO and add it to the list.
                foreach (var loanApplication in filteredList)
                    finalList.Add(LoanApplicationMapper.MapShowLoanApplicationDTO(loanApplication));

                //Return the list of ShowLoanApplicationDTO objects.
                return finalList;
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<ShowLoanApplicationDTO?> GetById(int id) {
            try {
                var loan = await _loanApplicationRepository.GetById(id);

                if(loan == null) return null;

                return LoanApplicationMapper.MapShowLoanApplicationDTO(loan);
            }
            catch {
                return null;
            }
        }

        public async Task<bool> Create(CreateLoanApplicationDTO oCreateLoanApplicationDTO) {
            try {
                // Create loan application
                var loanApplication = LoanApplicationMapper.MapLoanApplication(oCreateLoanApplicationDTO);
                loanApplication.DateApplication = DateOnly.FromDateTime(DateTime.Now);
                loanApplication.ApplicationStatusId = 1;

                // Get account associated with the loan application
                var account = await _accountRepository.GetByIdForLoanApplication(loanApplication.AccountId);

                // Create titles for the documents
                string employerOrderTitle = $"EmployerOrder-{account!.Customer.PersonId}" +
                    $"-{loanApplication.DateApplication.ToString("dd-MM-yy").Replace('/', '-')}";

                string salaryStatementTitle = $"SalaryStatement-{account.Customer.PersonId}" +
                    $"-{loanApplication.DateApplication.ToString("dd-MM-yy").Replace('/', '-')}";

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
            catch {
                // Handle errors and log them appropriately
                _logger.LogError("----- Create Loan Application: An error occurred while creating and saving to the database. More about error:");
                return false;
            }
        }

        public byte[] ConvertToPdf(FileUploadDTO oFileUploadDTO, string title) {

            return oFileUploadDTO.ContentType.ToLower() switch {
                "text/plain" => FileConverterHelper.ConvertTxtToPdf(oFileUploadDTO, title),
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => FileConverterHelper.ConvertWordToPdf(oFileUploadDTO, title),
                _ => FileConverterHelper.CopyPdf(oFileUploadDTO, title),// Convert to PDF by default if the file extension is not supported
            };
        }

        //REPORTS
        public async Task<List<ShowLoanApplicationReviewedDTO>> GetAllRejectedLoanApplication() {
            try {
                var loanApplicationList = await _loanApplicationRepository.GetAllAsync();

                var filteredList = loanApplicationList.Where(x => x.ApplicationStatusId == 3);

                var finalList = new List<ShowLoanApplicationReviewedDTO>();

                foreach (var loanApplication in filteredList)
                    finalList.Add(LoanApplicationMapper.MapShowLoanApplicationReviewedDTO(loanApplication));

                return finalList;
            } catch{
                return [];
            }
        }

        public async Task<List<ShowLoanApplicationReviewedDTO>> GetAllApprovedLoanApplication()
        {
            try
            {
                var loanApplicationList = await _loanApplicationRepository.GetAllAsync();

                var filteredList = loanApplicationList.Where(x => x.ApplicationStatusId == 2);

                var finalList = new List<ShowLoanApplicationReviewedDTO>();

                foreach (var loanApplication in filteredList)
                    finalList.Add(LoanApplicationMapper.MapShowLoanApplicationReviewedDTO(loanApplication));

                return finalList;
            }
            catch
            {
                return [];
            }
        }
    }
}