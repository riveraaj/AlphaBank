using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using Mapper.Common;
using Microsoft.Extensions.Logging;
using Service.Common.Helpers;

namespace Service.Common {
    public class ContractService (IContractRepository contractRepository,
                                ILogger<ContractService> logger) : IContractService {

        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly ILogger<ContractService> _logger = logger;

        public async Task<bool> LoanTypeCreate (LoanApplication oLoanApplication)  {
            try {
                Contract contract = new() {
                    DateStart = DateOnly.FromDateTime(DateTime.UtcNow)
                };

                contract.DateCompletion = contract.DateStart.AddMonths(int.Parse(oLoanApplication.Deadline.Description.Split(' ')[0]));

                contract.DateUpdate = contract.DateStart;

                contract.TypeContractId = 1;

                contract.LoanApplicationId = oLoanApplication.Id;

                // Create the Contract PDF and get the file path.
                contract.PathFile = LoanContractCreationHelper.CreatePdf(oLoanApplication);
                // Check if the Contract PDF was created
                if (string.IsNullOrEmpty(contract.PathFile)) return false;

                _logger.LogInformation("----- Create Contract: Start the creation of an contract registry");

                await _contractRepository.CreateAsync(contract);
                await _contractRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Contract: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("----- Create Contract: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<ShowContractDTO>> GetByLoanApplicationID(int id) {
            try {
                //Retrieve Contracts by LoanApplicationId asynchronously from the ContractRepository.
                var contractList = await _contractRepository.GetByLoanApplicationId(id);

                //Initialize a list to store ShowContractDto objects.
                var showContractDtoList = new List<ShowContractDTO>();
                foreach (var contract in contractList)
                    showContractDtoList.Add(ContractMapper.MapShowContractDTO(contract));

                // Return the list of ShowContractDto objects.
                return showContractDtoList;
            } catch {
                return [];
            }
        }

        public async Task<Contract?> GetByLoanApplicationId(int id) {
            try {
                //Get loan application by id.
                return await _contractRepository.GetByLoanApplicationID(id);
            } catch {
                // If there's an exception during the process, return an empty list.
                return null;
            }
        } 
    }
}