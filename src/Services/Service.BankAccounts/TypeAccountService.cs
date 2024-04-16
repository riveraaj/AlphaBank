using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;
using Mapper.BankAccounts;
using Microsoft.Extensions.Logging;

namespace Service.BankAccounts {
    public class TypeAccountService (ITypeAccountRepository typeAccountRepository, ILogger<TypeAccountService> logger)
                                       : ITypeAccountService {

        private readonly ITypeAccountRepository _typeAccountRepository
            = typeAccountRepository;
        private readonly ILogger<TypeAccountService> _logger = logger;


        public async Task<UpdateTypeAccountDTO?> GetById(int id) {
            try {
                var typeAccount = await _typeAccountRepository.GetById(id);
                return TypeAccountMapper.MapUpdateTypeAccount(typeAccount!);
            } catch {
                return null;
            }
        }

        public async Task<List<ShowCatalogsDTO>> GetAllForShow() {
            try  {
                //Attempt to retrieve all deadline asynchronously from the DeadlineRepository.
                var deadlineList = await _typeAccountRepository.GetAllAsync();
                var showDeadlineList = new List<ShowCatalogsDTO>();
                foreach (var deadline in deadlineList)
                    showDeadlineList.Add(TypeAccountMapper.MapShowTypeAccountDTO(deadline));

                return showDeadlineList;
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Create(CreateTypeAccountDTO oCreateTypeAccountDTO)
        {
            // Map CreateTypeAccountDTO to a typeAccount object using some mapper (TypeAccountMapper)
            var typeAccount = TypeAccountMapper.MapTypeAccount(oCreateTypeAccountDTO);

            try
            {

                _logger.LogInformation("----- Create TypeAccount: Start creating and saving the typeAccount in the database");

                // Attempt to add the new typeAccount through the TypeAccountRepository and save changes asynchronously.
                await _typeAccountRepository.CreateAsync(typeAccount);

                await _typeAccountRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create TypeAccount: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            }
            catch
            {
                _logger.LogError("----- Create TypeAccount: An error occurred while creating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<TypeAccount>> GetAll() {
            try {
                //Retrieve all TypeAccount asynchronously from the TypeAccountRepository.
                return (List<TypeAccount>) await _typeAccountRepository.GetAllAsync();
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

        public async Task<bool> Update(UpdateTypeAccountDTO oUpdateTypeAccountDTO)
        {
            // Map UpdateTypeAccountDTO to a typeAccount object using some mapper (TypeAccountMapper)
            var typeAccount = TypeAccountMapper.MapUpdateTypeAccount(oUpdateTypeAccountDTO);

            try
            {

                _logger.LogInformation("----- Update TypeAccount: Start updating and saving the typeAccount in the database");

                // Attempt to update a typeAccount through the TypeAccountRepository and save changes asynchronously.
                await _typeAccountRepository.UpdateAsync(typeAccount);

                await _typeAccountRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update TypeAccount: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Update TypeAccount: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                _logger.LogInformation("----- Remove TypeAccount: Start removing a typeAccount and saving the changes in the database");

                // Attempt to remove a typeAccount through the TypeAccountRepository and save changes asynchronously.
                await _typeAccountRepository.RemoveAsync(id);

                await _typeAccountRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove TypeAccount: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Remove TypeAccount: An error occurred while removing a typeAccount and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }
    }
}