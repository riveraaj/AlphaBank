﻿using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.Common.Services;
using Interfaces.GrantingLoans.Services;
using Microsoft.Extensions.Logging;

namespace Service.GrantingLoans
{
    public class GrantingLoansService (ILoanApplicationRepository loanApplicationRepository,
                                       IContractService contractService,
                                       ILogger<GrantingLoansService> logger) : IGrantingLoansService {

        private readonly ILoanApplicationRepository _loanApplicationRepository = loanApplicationRepository;
        private readonly IContractService _contractService = contractService;
        private readonly ILogger<GrantingLoansService> _logger = logger;

        public async Task<bool> GrantingLoan(int idLoanApplication) {
			try {
                _logger.LogInformation("----- Loan Granting: Start the process of granting a loan.");

                _logger.LogInformation("----- Loan Granting: Lookup of Loan Application by ID.");
                //Get LoanApplication Objetct by ID
                var oLoanApplication = await _loanApplicationRepository.GetByIdForContract(idLoanApplication);
                // Check if we Succesfully get the LoanApplication Object ID
                if (oLoanApplication == null) return false;

                _logger.LogInformation("----- Loan Granting: Create a registry of Contract.");
                //Create the Contract PDF File and the Contract in the DataBase
                var result = await _contractService.LoanTypeCreate(oLoanApplication);
                // Check if we Succesfully create the Contract PDF File and the Contract in the DataBase
                if (!result) return false;

                _logger.LogInformation("----- Loan Granting: Update the ApplicationStatus of the LoanApplication.");
                //Update the LoanApplication Status to 2 or "Approbado"
                await _loanApplicationRepository.UpdateApplicationStatus(oLoanApplication.Id, 2);
                await _loanApplicationRepository.SaveChangesAsync();

                _logger.LogInformation("----- Loan Granting: Create the Loan in the data base.");
                //Create Loan Record in the Data Base
                /* AQUI VA EL INSERT EN LA TABLA LOAN */
                //Check if we Succesfully create Loan Record in the Data Base
                /* AQUI REVISO EL INSERT EN LA TABLA LOAN */

                _logger.LogInformation("----- Loan Granting: Loan Granting process completed successfully.");

                return true;
			} catch (Exception e) {
                _logger.LogError($"----- Loan Granting: An error occurred while granting loan. More about error: {e.Message}");

                //If there's an exception during the process, return false.
                return false;
			}
        }
    }
}