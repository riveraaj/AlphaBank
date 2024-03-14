using Data.AlphaBank;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts;
using Interfaces.Security;
using Mapper.BankAccounts;
using Microsoft.Extensions.Logging;

namespace Service.BankAccounts
{
    public class CustomerService(IUnitOfWork unitOfWork,
                                 IPersonRepository personRepository,
                                 ICustomerRepository customerRepository,
                                 IOccupationRepository occupationRepository,
                                 ICustomerStatusRepository customerStatusRepository,
                                 IPhoneRepository phoneRepository/*,
                                 ILogger<CustomerService> logger*/) : ICustomerService
                                 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IOccupationRepository _occupationRepository = occupationRepository;
        private readonly ICustomerStatusRepository _customerstatusRepository = customerStatusRepository;
        private readonly IPhoneRepository _phoneRepository = phoneRepository;
        //private readonly ILogger<CustomerService> _logger = logger;

        public async Task<bool> Create(CreateCustomerDto createCustomerDto)
        {
            // Map CreateCustomerDto to person, customer, and phone objects using CustomerMapper.
            var person = CustomerMapper.MapPerson(createCustomerDto);
            var phone = CustomerMapper.MapPhone(createCustomerDto);
            var customer = CustomerMapper.MapCustomer(createCustomerDto); 

            // Set the status of the customer to true.
            customer.Status = true;

            // Set the deceased attribute of the person to false.
            person.Deceased = false;

            try
            {
                //_logger.LogInformation("---- Start the transaction to create and save in the database an person, phone number and customer.");

                //Begin a transaction using the unit of work.
                await _unitOfWork.BeginTransaction();

                //Create records in the PersonRepository, PhoneRepository, and CustomerRepository.
                await _personRepository.CreateAsync(person);
                await _phoneRepository.CreateAsync(phone);
                await _customerRepository.CreateAsync(customer);

                //Commit the transaction and save changes.
                await _unitOfWork.CommitTransaction();
                await _unitOfWork.SaveChangesAsync();

                //_logger.LogInformation("---- Correctly completes the transaction.");

                // Return true to indicate successful creation.
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError($"--- An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, rollback the transaction and return false.
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        public async Task<List<Occupation>> GetAllOccupations()
        {
            try
            {
                //Attempt to retrieve all occupations asynchronously from the OccupationRepository.
                return (List<Occupation>)await _occupationRepository.GetAllAsync();

            }
            catch (Exception)
            {
                //If there's an exception during the process, return null.
                return [];
            }
        }

        public async Task<List<CustomerStatus>> GetAllCustomerStatuses()
        {
            try
            {
                //Attempt to retrieve all customer statuses asynchronously from the CustomerStatuseRepository.
                return (List<CustomerStatus>)await _customerstatusRepository.GetAllAsync();

            }
            catch (Exception)
            {
                //If there's an exception during the process, return null.
                return [];
            }
        }

    }
}
