using Data.AlphaBank;
using Dtos.AlphaBank.Common;
using Interfaces.Common;
using Interfaces.Security;
using Mapper.Common;
using Microsoft.Extensions.Logging;

namespace Service.Common {
    public class PersonService(/*ILogger<PersonService> logger,*/
                                IPersonRepository personRepository,
                                IPhoneRepository phoneRepository,
                                IUnitOfWork unitOfWork) : IPersonService {

        //private readonly ILogger<PersonService> _logger = logger;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IPhoneRepository _phoneRepository = phoneRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Person?> GetById(int id) 
            => await _personRepository.GetByIdAsync(id);

        public async Task<bool> Create(CreatePersonDto oCreatePersonDto, 
                                        CreatePhoneDto oCreatePhoneDto) {

            // Map CreateEmployeeDto to person and phone
            var person = PersonMapper.MapPerson(oCreatePersonDto);
            var phone = PhoneMapper.MapPhone(oCreatePhoneDto);

            // Set the Deceased of the person to false.
            person.Deceased = false;

            try {

                //_logger.LogInformation("----- Create Person: Start the transaction to create a person and phone registry");

                //Begin a transaction using the unit of work.
                await _unitOfWork.BeginTransaction();

                //Create records in the PersonRepository and PhoneRepository.
                await _personRepository.CreateAsync(person);
                await _phoneRepository.CreateAsync(phone);

                //Commit the transaction and save changes.
                await _unitOfWork.CommitTransaction();

                //_logger.LogInformation("----- Create Person: Successfully completes saving to the transaction database.");

                // Return true to indicate successful creation.
                return true;
            } catch (Exception e) {

                //_logger.LogError($"--- Create Person: An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, rollback the transaction and return false.
                return false;
            }
        }
    }
}