using Data.AlphaBank;
using Data.AlphaBank.Enums;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.BankAccounts.Repositories;
using Interfaces.Common.Services;
using Interfaces.GrantingLoans.Services;
using Microsoft.Extensions.Logging;
using Service.Common.Helpers;

namespace Service.GrantingLoans {
    public class GrantingLoansService (ILoanApplicationRepository loanApplicationRepository,
                                       IAccountRepository accountRepository,
                                       IContractService contractService,
                                       ILoanService loanService,
                                       IMailService mailService,
                                       INotificationService notificationService,
                                       ILogger<GrantingLoansService> logger) : IGrantingLoansService {

        private readonly ILoanApplicationRepository _loanApplicationRepository = loanApplicationRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IContractService _contractService = contractService;
        private readonly ILoanService _loanService = loanService;
        private readonly IMailService _mailService = mailService;
        private readonly INotificationService _notificationService = notificationService;
        private readonly ILogger<GrantingLoansService> _logger = logger;

        public async Task<bool> GrantingLoan(int idLoanApplication, bool grantingLoan) {
			try {
                _logger.LogInformation("----- Loan Granting: Start the process of granting a loan.");

                _logger.LogInformation("----- Loan Granting: Lookup of Loan Application by ID.");
                //Get LoanApplication Objetct by ID
                var oLoanApplication = await _loanApplicationRepository.GetByIdForContract(idLoanApplication);
                // Check if we Succesfully get the LoanApplication Object ID
                if (oLoanApplication == null) return false;

                // Check if the ApplicationStatusId is only 1 (Pendiente), if the Loan Application is not Pending the Loan can´t be granted
                if (oLoanApplication.ApplicationStatusId != 1) return false;

                //Obtaining the customer's mail
                var email = oLoanApplication.Account.Customer.EmailAddress;
                var customer = oLoanApplication.Account.Customer.Person;

                string messageTemplate;
                string formattedMessage;
                // In case the grantingLoan boolean value is False, the Loan will be Denied and only the status will be updated
                if (!grantingLoan) {
                    _logger.LogInformation("----- Loan Granting: Update the ApplicationStatus of the LoanApplication.");
                    //Update the LoanApplication Status to 3 or "Denegado"
                    await _loanApplicationRepository.UpdateApplicationStatus(oLoanApplication.Id, 3);
                    await _loanApplicationRepository.SaveChangesAsync();


                    messageTemplate = await _notificationService.GetMessageById
                                                  ((int)TypeNotificationEnum.NotificaciónDeSolicitudDePrestamoRechazada) ?? " ";

                    messageTemplate = messageTemplate.Replace("\\n", "<br>");

                    formattedMessage = messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Name} {customer.FirstName}")
                                                             .Replace("[ID del Préstamo]", oLoanApplication.Id.ToString());

                    await _mailService.SendEmailAsync(email, "Solicitud de Préstamo Rechazada | AlphaBank", formattedMessage);

                    _logger.LogInformation("----- Loan Granting: Loan Granting process completed successfully.");
                    return true;
                }

                _logger.LogInformation("----- Loan Granting: Create the Loan in the data base.");
                //Create Loan Record in the Data Base
                var loanCreation = await _loanService.Create(oLoanApplication);
                //Check if we Succesfully create Loan Record in the Data Base
                if (!loanCreation) return false;

                _logger.LogInformation("----- Loan Granting: Create a registry of Contract.");
                //Create the Contract PDF File and the Contract in the DataBase
                var contractCreation = await _contractService.LoanTypeCreate(oLoanApplication);
                // Check if we Succesfully create the Contract PDF File and the Contract in the DataBase
                if (!contractCreation) return false;

                _logger.LogInformation("----- Loan Granting: Update the ApplicationStatus of the LoanApplication.");
                //Update the LoanApplication Status to 2 or "Approbado"
                await _loanApplicationRepository.UpdateApplicationStatus(oLoanApplication.Id, 2);
                await _loanApplicationRepository.SaveChangesAsync();

                var loanCreated = await _loanService.GetByLoanApplicationId(oLoanApplication.Id);
                if (loanCreated == null) return false;

                messageTemplate = await _notificationService.GetMessageById
                                                  ((int)TypeNotificationEnum.NotificaciónDeSolicitudDePrestamoAprobada) ?? " ";

                messageTemplate = messageTemplate.Replace("\\n", "<br>");

                formattedMessage = messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Name} {customer.FirstName}\n")
                                                         .Replace("[ID del Préstamo]", oLoanApplication.Id.ToString())
                                                         .Replace("[Monto del Préstamo]", MoneyFormatHelper.MoneyFormat(oLoanApplication.Amount.ToString(), oLoanApplication.TypeCurrency.Description))
                                                         .Replace("[Fecha de Aprobación]", DateTime.Now.ToString("dd/MM/yyyy"));

                await _mailService.SendEmailAsync(email, "Solicitud de Préstamo Aprobada | AlphaBank", formattedMessage);

                await _notificationService.Create(new Notification
                {
                    DateShipment = DateOnly.FromDateTime(DateTime.Today),
                    LoanId = loanCreated.Id,
                    TypeNotificationId = (byte)TypeNotificationEnum.NotificaciónDeSolicitudDePrestamoAprobada
                });

                _logger.LogInformation("----- Loan Granting: Loan Granting process completed successfully.");

                return true;
			} catch {
                _logger.LogError("----- Loan Granting: An error occurred while granting loan.");

                //If there's an exception during the process, return false.
                return false;
			}
        }

        public async Task<bool> AddLoanFunds(int idLoanApplication){
            try
            {
                _logger.LogInformation("----- Loan Granting: Start the process of adding funds into the account.");

                _logger.LogInformation("----- Loan Granting: Lookup of Loan Application by ID.");
                //Get LoanApplication Objetct by ID
                var oLoanApplication = await _loanApplicationRepository.GetByIdForContract(idLoanApplication);
                // Check if we Succesfully get the LoanApplication Object ID
                if (oLoanApplication == null) return false;

                // Check if the ApplicationStatusId is only 2 (Aprobado), if the Loan Application is not Approved we can´t add funds to the customer account.
                if (oLoanApplication.ApplicationStatusId != 2) return false;

                await _accountRepository.AddFundsAsync(oLoanApplication.Account.Id, oLoanApplication.Amount);
                await _accountRepository.SaveChangesAsync();

                _logger.LogInformation("----- Loan Granting: Adding funds into the account process completed successfully.");

                return true;
            }
            catch
            {
                _logger.LogError("----- Loan Granting: An error occurred while adding funds into the account.");

                //If there's an exception during the process, return false.
                return false;
            }
        }
    }
}