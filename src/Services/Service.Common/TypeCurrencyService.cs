using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using Mapper.Common;
using Microsoft.Extensions.Logging;

namespace Service.Common {
    public class TypeCurrencyService (ITypeCurrencyRepository typeCurrencyRepository, ILogger<TypeCurrencyService> logger)
                                        : ITypeCurrencyService {

        private readonly ITypeCurrencyRepository _typeCurrencyRepository
            = typeCurrencyRepository;
        private readonly ILogger<TypeCurrencyService> _logger = logger;

        public async Task<bool> Create(CreateTypeCurrencyDTO oCreateTypeCurrencyDTO)
        {
            // Map CreateTypeCurrencyDTO to a typeCurrency object using some mapper (TypeCurrencyMapper)
            var typeCurrency = TypeCurrencyMapper.MapTypeCurrency(oCreateTypeCurrencyDTO);

            try
            {

                _logger.LogInformation("----- Create TypeCurrency: Start creating and saving the typeCurrency in the database");

                // Attempt to add the new typeCurrency through the TypeCurrencyRepository and save changes asynchronously.
                await _typeCurrencyRepository.CreateAsync(typeCurrency);

                await _typeCurrencyRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create TypeCurrency: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            }
            catch
            {
                _logger.LogError("----- Create TypeCurrency: An error occurred while creating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<TypeCurrency>> GetAll() {
            try {
                //Retrieve all TypeCurrency asynchronously from the TypeCurrencyRepository.
                return (List<TypeCurrency>) await _typeCurrencyRepository.GetAllAsync();
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

        public async Task<bool> Update(UpdateTypeCurrencyDTO oUpdateTypeCurrencyDTO)
        {
            // Map UpdateTypeCurrencyDTO to a typeCurrency object using some mapper (TypeCurrencyMapper)
            var typeCurrency = TypeCurrencyMapper.MapUpdateTypeCurrency(oUpdateTypeCurrencyDTO);

            try
            {

                _logger.LogInformation("----- Update TypeCurrency: Start updating and saving the typeCurrency in the database");

                // Attempt to update a typeCurrency through the TypeCurrencyRepository and save changes asynchronously.
                await _typeCurrencyRepository.UpdateAsync(typeCurrency);

                await _typeCurrencyRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update TypeCurrency: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Update TypeCurrency: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                _logger.LogInformation("----- Remove TypeCurrency: Start removing a typeCurrency and saving the changes in the database");

                // Attempt to remove a typeCurrency through the TypeCurrencyRepository and save changes asynchronously.
                await _typeCurrencyRepository.RemoveAsync(id);

                await _typeCurrencyRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove TypeCurrency: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Remove TypeCurrency: An error occurred while removing a typeCurrency and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }
    }
}