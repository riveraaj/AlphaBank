using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using System.Diagnostics.Contracts;

namespace Service.Common
{
    public class LoanService (ILoanRepository loanRepository/*,
                               ILogger<AccountService> logger*/) : ILoanService
    {
        ILoanRepository _loanRepository = loanRepository;
        //private readonly ILogger<AccountService> _logger = logger;

        public async Task<bool> Create (LoanApplication oLoanApplication)
        {
            try
            {
                Loan loan = new();

                loan.RemainingQuotas = int.Parse(loan.LoanApplication.Deadline.Description.Split(' ')[0]);

                loan.LoanApplicationId = oLoanApplication.Id;

                loan.LoanStatementId = 1;

                //_logger.LogInformation("----- Create Loan: Start the creation of an loan registry");

                await _loanRepository.CreateAsync(loan);
                await _loanRepository.SaveChangesAsync();

                //_logger.LogInformation("----- Create Loan: Creation completed and saved successfully.");


                //Return true to indicate successful creation.
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError($"----- Create Loan: An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, return false.
                return false;
            }

        }
    }
}
