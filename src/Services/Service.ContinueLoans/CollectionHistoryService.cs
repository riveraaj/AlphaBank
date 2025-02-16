﻿using Data.AlphaBank;
using Data.AlphaBank.Enums;
using Dtos.AlphaBank.ContinueLoans;
using Interfaces.Common.Services;
using Interfaces.ContinueLoans.Repositories;
using Interfaces.ContinueLoans.Services;
using Mapper.ContinueLoans;
using Microsoft.Extensions.Logging;

namespace Service.ContinueLoans {
    public class CollectionHistoryService(ICollectionHistoryRepository collectionHistoryRepository,
                                          ILoanService loanService,
                                          IContractService contractService,
                                          ILogger<CollectionHistoryService> logger) : ICollectionHistoryService {

        private readonly ICollectionHistoryRepository _collectionHistoryRepository
            = collectionHistoryRepository;

        private readonly IContractService _contractService = contractService;

        private readonly ILoanService _loanService = loanService;

        private readonly ILogger<CollectionHistoryService> _logger = logger;

        public async Task<List<CollectionHistory>> GetAll() {
            try {
                //Retrieve Contracts by LoanApplicationId asynchronously from the ContractRepository.
                return (List<CollectionHistory>)await _collectionHistoryRepository.GetAllAsync();
            } catch {
                return [];
            }
        }

        public async Task<bool> Create(CreateCollectionHistoryDTO oCreateCollectionHistoryDTO) {
            try {
                //Get Loan By Id
                var loan = await _loanService.GetById((int) oCreateCollectionHistoryDTO.LoanId!);

                if(loan!.RemainingQuotas == 0) return false;

                //Get Contract By Loan Application
                var contract = await _contractService.GetByLoanApplicationId(loan!.LoanApplicationId);

                //Get last collection history
                var lastCollectionHistory = await _collectionHistoryRepository.GetLastByLoanId(loan!.Id);

                // Create collection history
                var collectionHistory = CollectionHistoryMapper.MapCollectionHistory(oCreateCollectionHistoryDTO);
                collectionHistory.DateDeposit = DateOnly.FromDateTime(DateTime.Now);

                if (lastCollectionHistory == null) {
                    collectionHistory.Deadline = contract!.DateStart.AddMonths(1);
                } else {
                    collectionHistory.Deadline = lastCollectionHistory.Deadline.AddMonths(1);
                    if ((lastCollectionHistory.Amount + lastCollectionHistory.MoratoriumInterest) != lastCollectionHistory.DepositMount)  {
                        decimal difference = (lastCollectionHistory.Amount + lastCollectionHistory.MoratoriumInterest) - lastCollectionHistory.DepositMount;
                        oCreateCollectionHistoryDTO.DepositAmount -= difference;

                        _logger.LogInformation("----- Update  Collection History: Start the creation of a collection history registry");

                        await _collectionHistoryRepository.UpdateAsync(lastCollectionHistory.Id, difference);
                        await _collectionHistoryRepository.SaveChangesAsync();

                        _logger.LogInformation("----- Update  Collection History: Creation completed and saved successfully.");
                    }
                }

                collectionHistory.QuotaNumber = loan.RemainingQuotas;
                collectionHistory.DepositMount = (decimal)oCreateCollectionHistoryDTO.DepositAmount!;
                collectionHistory.Amount = contract!.LoanApplication.Amount / int.Parse(contract!.LoanApplication.Deadline.Description.Split(' ')[0]);               
                collectionHistory.LoanId = loan.Id;
                int daysOverdue = CalculateDaysOverdue(collectionHistory.DateDeposit, collectionHistory.Deadline);
                collectionHistory.MoratoriumInterest = CalculateMoratoryInterest(collectionHistory.Amount, 0.05m, daysOverdue);

                _logger.LogInformation("----- Create  Collection History: Start the creation of a collection history registry");

                await _collectionHistoryRepository.CreateAsync(collectionHistory);
                await _collectionHistoryRepository.SaveChangesAsync();

                await _loanService.UpdateQuotas(loan.Id);

                if (loan!.RemainingQuotas == 1) await _loanService.UpdateStatement(loan.Id, 5);

                _logger.LogInformation("----- Create  Collection History: Creation completed and saved successfully.");
                return true;
            }
            catch(Exception) {
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

        //REPORTS
        public async Task<List<ShowLoanDefaultersDTO>> GetAllForDefaultersReport()
        {
            try
            {
                //Attempt to retrieve all loanS asynchronously from the LoanRepository.
                var loanList = await _loanService.GetAll();

                var filteredList = loanList.Where(x => x.LoanApplication.Account.Customer.CustomerStatusId == (byte)CustomerStatusEnum.Moroso);

                //Initialize a list to store ShowLoanDefaultersDTO objects.
                var finalList = new List<ShowLoanDefaultersDTO>();

                //Get todays date
                DateTime today = DateTime.UtcNow;

                //Map each loan to a ShowLoanDefaultersDTO and add it to the list.
                foreach (var loan in filteredList)
                {
                    var lastCollectionHistory = await _collectionHistoryRepository.GetLastByLoanId(loan.Id);

                    if (lastCollectionHistory == null)
                        continue;

                    string daysLateString = "";

                    //Calculate the difference in days between the deadline and today
                    DateTime lastDeadLine = new (lastCollectionHistory.Deadline.Year,
                                                         lastCollectionHistory.Deadline.Month,
                                                         lastCollectionHistory.Deadline.Day);
                    int daysLate = (today - lastDeadLine).Days;
                    //Make sure late days are never negative
                    daysLate = Math.Max(daysLate, 0);

                    //Format days late string
                    daysLateString = daysLate == 1 ? "1 día" : $"{daysLate} días";

                    finalList.Add(CollectionHistoryMapper.MapShowLoanDefaultersDTO(loan, daysLateString));
                }

                //Return the list of ShowLoanDefaultersDTO objects.
                return finalList;
            }
            catch
            {
                //If there's an exception during the process, return null.
                return [];
            }
        }

    }
}