﻿using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using Microsoft.Extensions.Logging;
using Service.BankAccounts;

namespace Service.Common {
    public class LoanService (ILoanRepository loanRepository,
                               ILogger<AccountService> logger) : ILoanService {

        private readonly ILoanRepository _loanRepository = loanRepository;
        private readonly ILogger<AccountService> _logger = logger;

        public async Task<bool> Create (LoanApplication oLoanApplication) {
            try {
                var existingLoans = await _loanRepository.GetByLoanApplicationId(oLoanApplication.Id);
                if (existingLoans != null) return false;

                Loan loan = new() {
                    RemainingQuotas = int.Parse(oLoanApplication.Deadline.Description.Split(' ')[0]),
                    LoanApplicationId = oLoanApplication.Id,
                    LoanStatementId = 1
                };

                _logger.LogInformation("----- Create Loan: Start the creation of an loan registry");

                await _loanRepository.CreateAsync(loan);
                await _loanRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Loan: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("----- Create Loan: An error occurred while creating and saving to the database. More about error");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task UpdateStatement(int id, byte loanStatementId) {
            try {
                _logger.LogInformation("----- Update Loan Statement: Start the updating of an loan registry");

                await _loanRepository.UpdateLoanStatementAsync(id, loanStatementId);
                await _loanRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Loan Statement: Update completed and saved successfully.");
            } catch {
                _logger.LogError("----- Update Loan Statement: An error occurred while updating and saving to the database.");
            }
        }

        public async Task UpdateQuotas(int id) {
            try {
                _logger.LogInformation("----- Update Loan Quotas: Start the updating of an loan registry");

                await _loanRepository.UpdateRemainingQuotasAsync(id);
                await _loanRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Loan Quotas: Update completed and saved successfully.");
            } catch {
                _logger.LogError("----- Update Loan Quotas: An error occurred while updating and saving to the database.");
            }
        }
    }
}