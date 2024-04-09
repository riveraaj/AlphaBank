using Dtos.AlphaBank.ContinueLoans;
using Interfaces.Common.Services;
using Interfaces.ContinueLoans.Repositories;
using Mapper.ContinueLoans;
using Microsoft.Extensions.Logging;

namespace Service.ContinueLoans {
    public class CollectionHistoryService(ICollectionHistoryRepository collectionHistoryRepository,
                                          ILoanService loanService,
                                          IContractService contractService,
                                          ILogger<CollectionHistoryService> logger) {

        private readonly ICollectionHistoryRepository _collectionHistoryRepository
            = collectionHistoryRepository;

        private readonly IContractService _contractService = contractService;

        private readonly ILoanService _loanService = loanService;

        private readonly ILogger<CollectionHistoryService> _logger = logger;

        public async Task<bool> Create(CreateCollectionHistoryDTO oCreateCollectionHistoryDTO) {
            try {
                //Get Loan By Id
                var loan = await _loanService.GetById((int) oCreateCollectionHistoryDTO.LoanId!);

                //Get Contract By Loan Application
                var contract = await _contractService.GetByLoanApplicationId(loan!.LoanApplicationId);

                // Create collection history
                var collectionHistory = CollectionHistoryMapper.MapCollectionHistory(oCreateCollectionHistoryDTO);
                collectionHistory.DateDeposit = DateOnly.FromDateTime(DateTime.Now);
                collectionHistory.Deadline = contract!.DateStart;
                collectionHistory.QuotaNumber = loan.RemainingQuotas;
                collectionHistory.Amount = (contract.LoanApplication.Amount / loan.RemainingQuotas);
                collectionHistory.DepositMount = (decimal) oCreateCollectionHistoryDTO.DepositAmount!;
                collectionHistory.LoanId = loan.Id;
                int daysOverdue = CalculateDaysOverdue(collectionHistory.DateDeposit, collectionHistory.Deadline);
                collectionHistory.MoratoriumInterest = CalculateMoratoryInterest(collectionHistory.Amount, 0.05m, daysOverdue);

                _logger.LogInformation("----- Create  Collection History: Start the creation of a collection history registry");

                await _collectionHistoryRepository.CreateAsync(collectionHistory);
                await _collectionHistoryRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create  Collection History: Creation completed and saved successfully.");
                return true;
            }
            catch {
                // Handle errors and log them appropriately
                _logger.LogError("----- Create Collection History: An error occurred while creating and saving to the database.");
                return false;
            }
        }

        private static int CalculateDaysOverdue(DateOnly depositDate, DateOnly deadline){
            DateTime dateTime1 = new (depositDate.Year, depositDate.Month, depositDate.Day);
            DateTime dateTime2 = new (deadline.Year, deadline.Month, deadline.Day);

            TimeSpan days = dateTime1 - dateTime2;

            return Math.Max(0, Convert.ToInt32(days.TotalDays));
        }

        private static decimal CalculateMoratoryInterest(decimal debt, decimal moratoryInterestRate, int daysOverdue) {
            int daysInYear = 365; // You can adjust this if you need to consider leap years
            return debt * moratoryInterestRate * ((decimal)daysOverdue / daysInYear);
        }
    }
}