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
                                     ILogger<ContinueLoanService> logger): IContinueLoanService {

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
                //Selects the last collectionHistory of each group
                var lastCollectionHistoryForEachLoan = groupedByLoanId.Select(group => {
                    //Finds the collectionHistory with the most recent date within the group
                    var lastCollectionHistory = group.OrderByDescending(ch => ch.Deadline).FirstOrDefault();
                    return lastCollectionHistory;
                }).ToList();

                lastCollectionHistoryForEachLoan.ForEach(async collectionHistory => {

                    _logger.LogInformation("----- Collection History: Payment validation begins.");

                    if (collectionHistory != null) { //Validate that the record is not invalid

                        //Obtaining the customer's mail
                        var email = collectionHistory.Loan.LoanApplication.Account.Customer.EmailAddress;
                        var customer = collectionHistory.Loan.LoanApplication.Account.Customer.Person;

                        //Validating that a payment is pending payment as long as the cutoff date is not passed
                        if (collectionHistory.Deadline.AddMonths(1) >= DateOnly.FromDateTime(DateTime.Today)) {

                            //The loan status is updated to payment on hold.
                            await _loanService.UpdateStatement(collectionHistory.LoanId,
                                (byte)LoanStatementEnum.EnEsperaDePago);

                            //A customer's status is updated to regular
                            await _customerService.Update(collectionHistory.Loan.LoanApplication.Account.CustomerId,
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
                        //It is validated that the loan payment is the last payment by installments.
                        else if (collectionHistory.Loan.LoanStatementId != (byte)LoanStatementEnum.Finalizado 
                                    && collectionHistory.Loan.RemainingQuotas == 0){

                            await _loanService.UpdateStatement(collectionHistory.LoanId,
                                (byte)LoanStatementEnum.Finalizado);

                            await _customerService.Update(collectionHistory.Loan.LoanApplication.Account.CustomerId,
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
                                TypeNotificationId = (byte) TypeNotificationEnum.NotificacionDeFinalizacionDePago
                            });
                        }
                        //Validate that the loan payment is overdue more than five days after the cutoff date.
                        else if (collectionHistory.Deadline.AddMonths(1).AddDays(5) <= DateOnly.FromDateTime(DateTime.Today)
                                    && collectionHistory.Loan.LoanStatementId == (byte) LoanStatementEnum.EnEsperaDePago) {

                            await _loanService.UpdateStatement(collectionHistory.LoanId,
                                (byte)LoanStatementEnum.RetrasoDePago);

                            await _customerService.Update(collectionHistory.Loan.LoanApplication.Account.CustomerId,
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
                                TypeNotificationId = (byte) TypeNotificationEnum.AvisoDeAtrasoEnElPago
                            });
                        }
                        //It is validated that the loan payment is overdue after 15 days from the cutoff
                        //to proceed to judicial collection.
                        else if (collectionHistory.Deadline.AddMonths(1).AddDays(15) < DateOnly.FromDateTime(DateTime.Today)
                                    && collectionHistory.Loan.LoanStatementId == (byte)LoanStatementEnum.RetrasoDePago) {

                            await _loanService.UpdateStatement(collectionHistory.LoanId,
                                (byte)LoanStatementEnum.EnProcesoCobroJudicial);

                            await _customerService.Update(collectionHistory.Loan.LoanApplication.Account.CustomerId,
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
                    _logger.LogInformation("----- Collection History: The task was successfully completed.");
                });
                _logger.LogInformation("----- Payment Background Task: The task was successfully completed.");
            } catch {
                _logger.LogError("----- Payment Background Task: An error occurred while creating and saving to the database.");
            }      
        }
    }
}