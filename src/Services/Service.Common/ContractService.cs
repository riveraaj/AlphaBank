using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using Microsoft.IdentityModel.Tokens;
using Service.Common.Helpers;

namespace Service.Common
{
    public class ContractService (IContractRepository contractRepository/*,
                                ILogger<AccountService> logger*/) : IContractService
    {
        public readonly IContractRepository _contractRepository = contractRepository;
        //public readonly ILogger<AccountService> _logger = logger;

        public async Task<bool> LoanTypeCreate (LoanApplication oLoanApplication)
        {
            try
            {
                Contract contract = new Contract ();

                contract.DateStart = DateOnly.FromDateTime(DateTime.UtcNow);

                contract.DateCompletion = contract.DateStart.AddMonths(int.Parse(oLoanApplication.Deadline.Description.Split(' ')[0]));

                contract.DateUpdate = contract.DateStart;

                contract.TypeContractId = 1;

                contract.LoanApplicationId = oLoanApplication.Id;

                // Create the Contract PDF and get the file path.
                contract.PathFile = LoanContractCreationHelper.CreatePdf(oLoanApplication);
                // Check if the Contract PDF was created
                if (string.IsNullOrEmpty(contract.PathFile)) return false;

                //_logger.LogInformation("----- Create Contract: Start the creation of an contract registry");

                await _contractRepository.CreateAsync(contract);
                await _contractRepository.SaveChangesAsync();

                //_logger.LogInformation("----- Create Contract: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError($"----- Create Contract: An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, return false.
                return false;
            }
        }

    }
}
