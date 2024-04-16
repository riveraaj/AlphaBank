using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Mapper.AnalyzeLoanOpportunities;
using Microsoft.Extensions.Logging;

namespace Service.AnalyzeLoanOpportunities {
    public class InterestService(IInterestRepository interestRepository, ILogger<InterestService> logger) : IInterestService {

        private readonly IInterestRepository _interestRepository = interestRepository;
        private readonly ILogger<InterestService> _logger = logger;

        public async Task<UpdateInterestDTO?> GetById(int id) {
            try {
                var interest = await _interestRepository.GetById(id);
                return InterestMapper.MapUpdateInterest(interest!);
            } catch {
                return null;
            }
        }

        public async Task<List<ShowCatalogsDTO>> GetAllForShow() {
            try {
                //Attempt to retrieve all deadline asynchronously from the DeadlineRepository.
                var deadlineList = await _interestRepository.GetAllAsync();
                var showDeadlineList = new List<ShowCatalogsDTO>();
                foreach (var deadline in deadlineList)
                    showDeadlineList.Add(InterestMapper.MapShowInterestDTO(deadline));

                return showDeadlineList;
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Create(CreateInterestDTO oCreateInterestDTO) {
            // Map CreateInterestDTO to a interest object using some mapper (InterestMapper)
            var interest = InterestMapper.MapInterest(oCreateInterestDTO);

            try {

                _logger.LogInformation("----- Create Interest: Start creating and saving the interest in the database");

                // Attempt to add the new interest through the InterestRepository and save changes asynchronously.
                await _interestRepository.CreateAsync(interest);

                await _interestRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Interest: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("----- Create Interest: An error occurred while creating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<Interest>> GetAll() {
            try {
                //Attempt to retrieve all interest asynchronously from the InterestRepository.
                return (List<Interest>)await _interestRepository.GetAllAsync();
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Update(UpdateInterestDTO oUpdateInterestDTO) {
            // Map UpdateInterestDTO to a interest object using some mapper (InterestMapper)
            var interest = InterestMapper.MapUpdateInterest(oUpdateInterestDTO);

            try {

                _logger.LogInformation("----- Update Interest: Start updating and saving the interest in the database");

                // Attempt to update a interest through the InterestRepository and save changes asynchronously.
                await _interestRepository.UpdateAsync(interest);

                await _interestRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Interest: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            } catch {
                _logger.LogError("----- Update Interest: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<bool> Remove(int id) {
            try {
                _logger.LogInformation("----- Remove Interest: Start removing a interest and saving the changes in the database");

                // Attempt to remove a interest through the InterestRepository and save changes asynchronously.
                await _interestRepository.RemoveAsync(id);

                await _interestRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove Interest: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            } catch {
                _logger.LogError("----- Remove Interest: An error occurred while removing a interest and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }
    }
}