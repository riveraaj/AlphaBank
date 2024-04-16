using Data.AlphaBank;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.Common;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Mapper.AnalyzeLoanOpportunities;
using Microsoft.Extensions.Logging;

namespace Service.AnalyzeLoanOpportunities {
    public class TypeLoanService(ITypeLoanRepository typeLoanRepository, ILogger<TypeLoanService> logger) : ITypeLoanService {

        private readonly ITypeLoanRepository _typeLoanRepository = typeLoanRepository;
        private readonly ILogger<TypeLoanService> _logger = logger;

        public async Task<UpdateTypeLoanDTO?> GetById(int id) {
            try {
                var typeLoan = await _typeLoanRepository.GetById(id);
                return TypeLoanMapper.MapUpdateTypeLoan(typeLoan!);
            } catch {
                return null;
            }
        }

        public async Task<List<ShowCatalogsDTO>> GetAllForShow()  {
            try {
                //Attempt to retrieve all deadline asynchronously from the DeadlineRepository.
                var deadlineList = await _typeLoanRepository.GetAllAsync();
                var showDeadlineList = new List<ShowCatalogsDTO>();
                foreach (var deadline in deadlineList)
                    showDeadlineList.Add(TypeLoanMapper.MapShowTypeLoanDTO(deadline));

                return showDeadlineList;
            } catch {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Create(CreateTypeLoanDTO oCreateTypeLoanDTO)
        {
            // Map CreateTypeLoanDto to a type loan object using some mapper (TypeLoanMapper)
            var typeLoan = TypeLoanMapper.MapTypeLoan(oCreateTypeLoanDTO);

            try
            {

                _logger.LogInformation("----- Create TypeLoan: Start creating and saving the type of loan in the database");

                // Attempt to add the new type of loan through the TypeLoanRepository and save changes asynchronously.
                await _typeLoanRepository.CreateAsync(typeLoan);

                await _typeLoanRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create TypeLoan: Successfully completes the process");

                // Return true to indicate successful creation.
                return true;
            }
            catch
            {
                _logger.LogError("----- Create TypeLoan: An error occurred while creating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<List<TypeLoan>> GetAll()
        {
            try
            {
                //Attempt to retrieve all typeLoan asynchronously from the TypeLoanRepository.
                return (List<TypeLoan>)await _typeLoanRepository.GetAllAsync();
            }
            catch
            {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<bool> Update(UpdateTypeLoanDTO oUpdateTypeLoanDTO)
        {
            // Map UpdateTypeLoanDto to a type of loan object using some mapper (TypeLoanMapper)
            var typeLoan = TypeLoanMapper.MapUpdateTypeLoan(oUpdateTypeLoanDTO);

            try
            {

                _logger.LogInformation("----- Update TypeLoan: Start updating and saving the type of loan in the database");

                // Attempt to update a type of loan through the TypeLoanRepository and save changes asynchronously.
                await _typeLoanRepository.UpdateAsync(typeLoan);

                await _typeLoanRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update TypeLoan: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Update TypeLoan: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;


            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                _logger.LogInformation("----- Remove TypeLoan: Start removing a type of loan and saving the changes in the database");

                // Attempt to remove a type of loan through the TypeLoanRepository and save changes asynchronously.
                await _typeLoanRepository.RemoveAsync(id);

                await _typeLoanRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove TypeLoan: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Remove TypeLoan: An error occurred while removing a type of loan and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }

    }
}