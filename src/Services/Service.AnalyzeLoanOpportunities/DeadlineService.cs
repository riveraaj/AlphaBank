using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Mapper.AnalyzeLoanOpportunities;
using Microsoft.Extensions.Logging;

namespace Service.AnalyzeLoanOpportunities {
    public class DeadlineService(IDeadlineRepository deadlineRepository, ILogger<DeadlineService> logger) : IDeadlineService {

        private readonly IDeadlineRepository _deadlineRepository = deadlineRepository;
        private readonly ILogger<DeadlineService> _logger = logger;

        public async Task<UpdateDeadlineDTO?> GetById(int id) {
            try  {
                var deadline = await _deadlineRepository.GetById(id);
                return DeadlineMapper.MapUpdateDeadline(deadline!);
            } catch {
                return null;
            }
        }

        public async Task<bool> Create(CreateDeadlineDTO oCreateDeadlineDTO) {
            // Map CreateDeadlineDTO to a deadline object using some mapper (DeadlineMapper)
            var deadline = DeadlineMapper.MapDeadline(oCreateDeadlineDTO);

            try {

                _logger.LogInformation("----- Create Deadline: Start creating and saving the deadline in the database");

                // Attempt to add the new deadline through the DeadlineRepository and save changes asynchronously.
                await _deadlineRepository.CreateAsync(deadline);

                await _deadlineRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Deadline: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            }
            catch {
                _logger.LogError("----- Create Deadline: An error occurred while creating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<Deadline>> GetAll() {
            try {
                //Attempt to retrieve all deadline asynchronously from the DeadlineRepository.
                return (List<Deadline>)await _deadlineRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<List<ShowCatalogsDTO>> GetAllForShow() {
            try {
                //Attempt to retrieve all deadline asynchronously from the DeadlineRepository.
                var deadlineList = await _deadlineRepository.GetAllAsync();
               var showDeadlineList = new List<ShowCatalogsDTO>();
                foreach (var deadline in deadlineList)
                    showDeadlineList.Add(DeadlineMapper.MapShowDeadlineDTO(deadline));

                return showDeadlineList;
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Update(UpdateDeadlineDTO oUpdateDeadlineDTO) {
            // Map UpdateDeadlineDTO to a deadline object using some mapper (DeadlineMapper)
            var deadline = DeadlineMapper.MapUpdateDeadline(oUpdateDeadlineDTO);

            try {

                _logger.LogInformation("----- Update Deadline: Start updating and saving the deadline in the database");

                // Attempt to update a deadline through the DeadlineRepository and save changes asynchronously.
                await _deadlineRepository.UpdateAsync(deadline);

                await _deadlineRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Deadline: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            } catch {
                _logger.LogError("----- Update Deadline: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<bool> Remove(int id) {
            try {
                _logger.LogInformation("----- Remove Deadline: Start removing a deadline and saving the changes in the database");

                // Attempt to remove a deadline through the DeadlineRepository and save changes asynchronously.
                await _deadlineRepository.RemoveAsync(id);

                await _deadlineRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove Deadline: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            } catch {
                _logger.LogError("----- Remove Deadline: An error occurred while removing a deadline and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }
    }
}