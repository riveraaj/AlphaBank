using Data.AlphaBank;
using Data.AlphaBank.Enums;
using Interfaces.BankAccounts.Services;
using Interfaces.Common.Services;
using Interfaces.ContinueLoans.Services;
using Microsoft.Extensions.Logging;

namespace Service.ContinueLoans {
    public class ContinueLoanService(ICollectionHistoryService collectionHistoryService,
                                     ICustomerService customerService,
                                     ILoanService loanService,
                                     IMailService mailService,
                                     INotificationService notificationService,
                                     ILogger<ContinueLoanService> logger) : IContinueLoanService {

        private readonly ICollectionHistoryService _collectionHistoryService = collectionHistoryService;
        private readonly ICustomerService _customerService = customerService;
        private readonly ILoanService _loanService = loanService;
        private readonly IMailService _mailService = mailService;
        private readonly INotificationService _notificationService = notificationService;
        private readonly ILogger<ContinueLoanService> _logger = logger;

        public async Task PaymentBackgroundTask() {
            try {

                _logger.LogInformation("----- Payment Background Task: The task of payment begins.");

                var collectionList = await _collectionHistoryService.GetAll();
                //Group By LoanId
                var groupedByLoanId = collectionList.GroupBy(ch => ch.LoanId);

                var tasks = groupedByLoanId.Select(async group => {
                    var lastCollectionHistory = group.OrderByDescending(ch => ch.Deadline).FirstOrDefault();
                    if (lastCollectionHistory != null) {
                        await ProcessCollectionHistory(lastCollectionHistory);
                    }
                });

                await Task.WhenAll(tasks);

                _logger.LogInformation("----- Payment Background Task: The task was successfully completed.");
            } catch {
                _logger.LogError("----- Payment Background Task: An error occurred while creating and saving to the database.");
            }
        }

        private async Task ProcessCollectionHistory(CollectionHistory collectionHistory) {
            _logger.LogInformation("----- Collection History: Payment validation begins.");

            // Obtaining the customer's mail
            var email = collectionHistory.Loan.LoanApplication.Account.Customer.EmailAddress;
            var customer = collectionHistory.Loan.LoanApplication.Account.Customer.Person;
            
            //It is validated that the loan payment is the last payment by installments.
            if (collectionHistory.Loan.LoanStatementId == (byte)LoanStatementEnum.Finalizado
                                    && collectionHistory.Loan.RemainingQuotas == 0) {
                await ProcessFinalPayment(collectionHistory, email, customer);
            }
            //Validating that a payment is pending payment as long as the cutoff date is not passed
            else if (collectionHistory.Deadline >= DateOnly.FromDateTime(DateTime.Today)) {
                await ProcessPaymentReminder(collectionHistory, email, customer);
            }
            //Validate that the loan payment is overdue more than five days after the cutoff date.
            else if (collectionHistory.Deadline.AddDays(5) <= DateOnly.FromDateTime(DateTime.Today)) {
                await ProcessPaymentDelay(collectionHistory, email, customer);
            }
            //It is validated that the loan payment is overdue after 15 days from the cutoff
            //to proceed to judicial collection.
            else if (collectionHistory.Deadline.AddDays(15) <= DateOnly.FromDateTime(DateTime.Today)
                        && collectionHistory.Loan.LoanStatementId == (byte)LoanStatementEnum.RetrasoDePago) {
                await ProcessJudicialCollection(collectionHistory, email, customer);
            }
            _logger.LogInformation("----- Collection History: The task was successfully completed.");
        }

        private async Task ProcessPaymentReminder(CollectionHistory collectionHistory, string email, Person customer) {
            //The loan status is updated to payment on hold.
            await _loanService.UpdateStatement(collectionHistory.LoanId,
                (byte)LoanStatementEnum.EnEsperaDePago);

            //A customer's status is updated to regular
            await _customerService.UpdateStatus(collectionHistory.Loan.LoanApplication.Account.CustomerId,
                (byte)CustomerStatusEnum.Regular);

            //The message to be sent by mail is obtained
            string messageTemplate = await _notificationService.GetMessageById(
                                                        (int)TypeNotificationEnum.RecordatorioDePago) ?? " ";

            messageTemplate = messageTemplate.Replace("\\n", "<br>");

            string formattedMessage = messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Name} {customer.FirstName}")
                                                     .Replace("[ID del Préstamo]", collectionHistory.LoanId.ToString())
                                                     .Replace("[Monto del Pago]", $"{collectionHistory.Loan.LoanApplication.TypeCurrency.Description} {collectionHistory.Amount}")
                                                     .Replace("[Fecha de Vencimiento]", collectionHistory.Deadline.ToString("d"));

            //Notification is sent to the customer.
            await _mailService.SendEmailAsync(email, "Recordatorio De Pago | AlphaBank", formattedMessage);

            await _notificationService.Create(new Notification {
                DateShipment = DateOnly.FromDateTime(DateTime.Today),
                LoanId = collectionHistory.LoanId,
                TypeNotificationId = (byte)TypeNotificationEnum.RecordatorioDePago
            });
        }

        private async Task ProcessFinalPayment(CollectionHistory collectionHistory, string email, Person customer) {
            await _loanService.UpdateStatement(collectionHistory.LoanId,
                                (byte)LoanStatementEnum.Finalizado);

            await _customerService.UpdateStatus(collectionHistory.Loan.LoanApplication.Account.CustomerId,
                (byte)CustomerStatusEnum.VIP);

            string messageTemplate = await _notificationService.GetMessageById
                                   ((int)TypeNotificationEnum.NotificacionDeFinalizacionDePago) ?? " ";

            messageTemplate = messageTemplate.Replace("\\n", "<br>");

            string formattedMessage = messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Name} {customer.FirstName}")
                                                     .Replace("[ID del Préstamo]", collectionHistory.LoanId.ToString());

            await _mailService.SendEmailAsync(email, "Finalización de Pago | AlphaBank", formattedMessage);

            await _notificationService.Create(new Notification {
                DateShipment = DateOnly.FromDateTime(DateTime.Today),
                LoanId = collectionHistory.LoanId,
                TypeNotificationId = (byte)TypeNotificationEnum.NotificacionDeFinalizacionDePago
            });
        }

        private async Task ProcessPaymentDelay(CollectionHistory collectionHistory, string email, Person customer) {
            await _loanService.UpdateStatement(collectionHistory.LoanId,
                                (byte)LoanStatementEnum.RetrasoDePago);

            await _customerService.UpdateStatus(collectionHistory.Loan.LoanApplication.Account.CustomerId,
                (byte)CustomerStatusEnum.EnRiesgo);

            string messageTemplate = await _notificationService.GetMessageById
                                   ((int)TypeNotificationEnum.AvisoDeAtrasoEnElPago) ?? " ";

            messageTemplate = messageTemplate.Replace("\\n", "<br>");

            string formattedMessage = messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Name} {customer.FirstName}")
                                                     .Replace("[ID del Préstamo]", collectionHistory.LoanId.ToString())
                                                     .Replace("[Fecha de Vencimiento]", collectionHistory.Deadline.ToString("d"));

            await _mailService.SendEmailAsync(email, "Atraso en el Pago | AlphaBank", formattedMessage);

            await _notificationService.Create(new Notification {
                DateShipment = DateOnly.FromDateTime(DateTime.Today),
                LoanId = collectionHistory.LoanId,
                TypeNotificationId = (byte)TypeNotificationEnum.AvisoDeAtrasoEnElPago
            });
        }

        private async Task ProcessJudicialCollection(CollectionHistory collectionHistory, string email, Person customer) {
            await _loanService.UpdateStatement(collectionHistory.LoanId,
                               (byte)LoanStatementEnum.EnProcesoCobroJudicial);

            await _customerService.UpdateStatus(collectionHistory.Loan.LoanApplication.Account.CustomerId,
                (byte)CustomerStatusEnum.Moroso);

            string messageTemplate = await _notificationService.GetMessageById
                                   ((int)TypeNotificationEnum.AvisoDeProcesoDeCobroJudicial) ?? " ";

            messageTemplate = messageTemplate.Replace("\\n", "<br>");

            string formattedMessage = messageTemplate.Replace("[Nombre del Cliente]", $"{customer.Name} {customer.FirstName}")
                                                     .Replace("[Fecha de Vencimiento]", collectionHistory.Deadline.ToString("d"));

            await _mailService.SendEmailAsync(email, "Proceso de Cobro Judicial | AlphaBank", formattedMessage);

            await _notificationService.Create(new Notification {
                DateShipment = DateOnly.FromDateTime(DateTime.Today),
                LoanId = collectionHistory.LoanId,
                TypeNotificationId = (byte)TypeNotificationEnum.AvisoDeProcesoDeCobroJudicial
            });
        }
    }
}