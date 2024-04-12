using Data.AlphaBank.Enums;
using Data.AlphaBank;
using Interfaces.Common.Services;
using Microsoft.Extensions.Logging;
using Dtos.AlphaBank.Common;
using Interfaces.BankAccounts.Services;
using Interfaces.RecoveringLoansService.Services;

namespace Service.RecoveringLoans {
    public class RecoveringLoansService(IMailService mailService,
                                        ILoanService loanService,
                                        INotificationService notificationService,
                                        IContractService contractService,
                                        ICustomerService customerService,
                                        ILogger<RecoveringLoansService> logger) : IRecoveringLoansService {

        private readonly IMailService _mailService = mailService;
        private readonly ILoanService _loanService = loanService;
        private readonly ICustomerService _customerService = customerService;
        private readonly IContractService _contractService = contractService;
        private readonly INotificationService _notificationService = notificationService;
        private readonly ILogger<RecoveringLoansService> _logger = logger;

        public async Task<bool> JudicialLoanProcess(int loanId, decimal amount) {
			try {
                _logger.LogInformation("----- Judicial Loan Process: The judicial collection process begins.");

                string collectionDepartmentEmail = "jonathandavidr7@gmail.com";
                var loan = await _loanService.GetById(loanId);
                var customer = loan?.LoanApplication.Account.Customer;

                if (loan != null && customer != null) {

                    await _loanService.UpdateStatement(loanId,
                                (byte)LoanStatementEnum.EnCobroJudicial);

                    await _customerService.Update(customer.Id,
                                (byte)CustomerStatusEnum.Suspendido);

                    string formattedMessage = await FormatCustomerMessageAsync(loan, customer);
                    await _mailService.SendEmailAsync(customer.EmailAddress, "Judicial Collection | AlphaBank", formattedMessage);

                    string formattedCollectionDepartmentMessage = await FormatCollectionDepartmentMessageAsync(loan, amount);
                    await _mailService.SendEmailAsync(collectionDepartmentEmail, "Judicial Collection | AlphaBank", formattedCollectionDepartmentMessage);

                    await _notificationService.Create(new Notification {
                        DateShipment = DateOnly.FromDateTime(DateTime.Today),
                        LoanId = loan.Id,
                        TypeNotificationId = (byte)TypeNotificationEnum.RecordatorioDePago
                    });

                    await _notificationService.Create(new Notification {
                        DateShipment = DateOnly.FromDateTime(DateTime.Today),
                        LoanId = loan.Id,
                        TypeNotificationId = (byte)TypeNotificationEnum.RecordatorioDePago
                    });

                    _logger.LogInformation("----- Judicial Loan Process: The process was successfully completed.");
                    return true;
                } else {
                    _logger.LogWarning("Judicial Loan Process: Loan or customer not found.");
                    return false;
                }           
            } catch {
                _logger.LogInformation("----- Judicial Loan Process: An error occurred while creating and saving to the database.");
                return false;
            }
        }

        public async Task<bool> GenerateNewContract(CreateRenegotiationContractDTO oCreateRenegotiationContractDTO){
            try {

                var loan = await _loanService.GetById(oCreateRenegotiationContractDTO.LoanId);

                await _loanService.UpdateStatement(loan!.Id,
                (byte)LoanStatementEnum.EnEsperaDePago);

                await _customerService.Update(loan.LoanApplication.Account.CustomerId,
                            (byte)CustomerStatusEnum.Regular);

                var result = await _contractService.RenegotiationTypeCreate
                                                    (loan!,
                                                     oCreateRenegotiationContractDTO.NewInterestPercentage.ToString()!,
                                                     oCreateRenegotiationContractDTO.NewLoanTerm.ToString()!,
                                                     (decimal) oCreateRenegotiationContractDTO.NewLoanAmount!);

                if(!result) return false;

                return true;
            } catch {
                return false;
            }
        }

        private async Task<string> FormatCustomerMessageAsync(Loan loan, Customer customer) {
            string messageTemplate = await _notificationService.GetMessageById((int)TypeNotificationEnum.AvisoDeCobroJudicial) ?? " ";
            return messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Person.Name} {customer.Person.FirstName}")
                                  .Replace("[ID del Préstamo]", loan.Id.ToString());
        }

        private async Task<string> FormatCollectionDepartmentMessageAsync(Loan loan, decimal amount) {
            string CollectionDepartmentMessageTemplate = await _notificationService.GetMessageById((int)TypeNotificationEnum.DepartamentoDeCobro) ?? " ";
            return CollectionDepartmentMessageTemplate
                   .Replace("[Monto Pendiente]", $"{loan.LoanApplication.TypeCurrency.Description} {amount}")
                   .Replace("[ID del Préstamo]", loan.Id.ToString())
                   .Replace("[Fecha de Inicio del Proceso Judicial]", DateTime.Today.ToString("yyyy-MM-dd"));
        }
    }
}